using System.Text.Json;
using CleanArchitecture.ApiShared.Interfaces;
using CleanArchitecture.ApiShared.Models;
using CleanArchitecture.ApiShared.Models.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
namespace CleanArchitecture.ApiShared.Helpers
{
    public sealed class TokenAuthentication : ITokenAuthentication
    {
        private readonly TokenProvider _tokenProvider;
        private readonly HttpClient _tokenClient;
        private AccessToken? _accessToken;
        private readonly ILogger<TokenAuthentication> _logger;
        public TokenAuthentication(IHttpClientFactory httpClientFactory,
            IOptions<TokenProvider> options, ILogger<TokenAuthentication> logger)
        {
            _tokenProvider = options.Value;
            _logger = logger;
            _tokenClient = httpClientFactory.CreateClient(nameof(TokenAuthentication));
            if (_tokenClient == null)
            {
                throw new ArgumentNullException(nameof(httpClientFactory));
            }
            if (!_tokenProvider.IsValid())
            {
                throw new ArgumentException(_tokenProvider.ValidateMessage);
            }
        }
        public async Task AddAuthenticationAsync(CustomHttpClient customHttpClient)
        {
            var accessToken = await GetAccessTokenAsync();
            customHttpClient.Client.DefaultRequestHeaders.Clear();
            string auth = $"Bearer {accessToken.Token}";
            customHttpClient.Client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", auth);
            customHttpClient.Client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", _tokenProvider.ClientId);
            if (accessToken.Token == null)
                throw new ArgumentException(message: "Token Authentication Failure");
        }
        public async Task<AccessToken> GetAccessTokenAsync(bool refresh = false)
        {
            if (_accessToken == null || _accessToken.ExpireDateTime < DateTime.UtcNow || refresh)
            {
                _logger.LogInformation("Fetching new access token. Refresh: {Refresh}, Current Time: {CurrentTime}, Token Expiry: {TokenExpiry}", refresh, DateTime.UtcNow, _accessToken?.ExpireDateTime);
                var contentPost = new FormUrlEncodedContent(
                [
                        new KeyValuePair<string, string>("grant_type", "client_credentials"),
                        new KeyValuePair<string, string>("client_id", _tokenProvider.ClientId),
                        new KeyValuePair<string, string>("client_secret", _tokenProvider.ClientSecret),
                        new KeyValuePair<string, string>("scope", _tokenProvider.Scope)
                    ]);
                _accessToken = await ExecuteWithRetryAsync(async () =>
                {
                    var response = await _tokenClient.PostAsync(new Uri($"{_tokenProvider.TokenUrl}/connect/token"), contentPost);
                    response.EnsureSuccessStatusCode();
                    var tokenResponse = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<AccessToken>(tokenResponse) ?? throw new ArgumentException("Deserialized token is null");
                });
            }
            return _accessToken ?? throw new InvalidOperationException("AccessToken is not set.");
        }
        private static async Task<T> ExecuteWithRetryAsync<T>(Func<Task<T>> action)
        {
            int retryCount = 0;
            int maxRetries = 2;
            TimeSpan delay = TimeSpan.FromSeconds(2);
            while (true)
            {
                try
                {
                    return await action();
                }
                catch (HttpRequestException) when (retryCount < maxRetries)
                {
                    retryCount++;
                    await Task.Delay(delay);
                    delay = TimeSpan.FromSeconds(Math.Pow(2, retryCount));
                }
                catch (TimeoutException) when (retryCount < maxRetries)
                {
                    retryCount++;
                    await Task.Delay(delay);
                    delay = TimeSpan.FromSeconds(Math.Pow(2, retryCount));
                }
                catch (TaskCanceledException) when (retryCount < maxRetries)
                {
                    retryCount++;
                    await Task.Delay(delay);
                    delay = TimeSpan.FromSeconds(Math.Pow(2, retryCount));
                }
            }
        }
    }
}
