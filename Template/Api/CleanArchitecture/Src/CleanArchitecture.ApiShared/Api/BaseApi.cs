using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using CleanArchitecture.ApiShared.Exceptions;
using CleanArchitecture.ApiShared.Helpers;
using CleanArchitecture.ApiShared.Interfaces;
using CleanArchitecture.ApiShared.Interfaces.Api;
using CleanArchitecture.ApiShared.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
namespace CleanArchitecture.ApiShared.Api
{
    public class BaseApi<T, TValue> : IApi<T, TValue>
    {
        private readonly ITokenAuthentication _tokenAuthentication;
        private readonly ApiConfiguration _apiConfiguration;
        public string BaseUrl { get; private set; }
        private readonly CustomHttpClient _customHttpClient;
        public ILogger Logger { get; private set; }
        public BaseApi(string baseUrl,
            CustomHttpClient customHttpClient,
            ITokenAuthentication tokenAuthentication,
            IOptions<ApiConfiguration> apiConfiguration,
            ILogger logger)
        {
            _apiConfiguration = apiConfiguration.Value;
            BaseUrl = baseUrl;
            _customHttpClient = customHttpClient;
            _tokenAuthentication = tokenAuthentication;
            Logger = logger;
            if (!_apiConfiguration.IsValid())
            {
                throw new ArgumentException(_apiConfiguration.ValidateMessage);
            }
        }
        public virtual async Task<T> GetAsync(TValue id)
        {
            var url = $"{BaseUrl}/Get/{id}";
            try
            {
                var response = await GetHttpContentAsync(url: url);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    (List<T>? values, List<ApiError>? errors, bool isSuccess, bool isFailure) results = BaseApi<T, TValue>.DeserializeJson(json);
                    if (results is { isSuccess: true, values: not null })
                        return results.values[0];
                    if (results.errors != null)
                        throw new ApiErrorException(results.errors);
                }
                await LogError(response);
            }
            catch (Exception ex)
            {
                if (ex is ApiErrorException)
                    throw;
                throw new ApiErrorException($"An error occurred while getting data: {ex.Message}");
            }
            return default!;
        }
        public virtual async Task<Page<T>> GetAsync(SearchRequest searchRequest)
        {
            //https://localhost:7187/api/Categories/Get?PageSize=0&SortOrderColumn=id&SortOrder=Descending&Skip=0
            var url = $"{BaseUrl}/Get";
            url = GetUrl(url);
            try
            {
                url = $"{url}?{searchRequest.ToQueryString()}";
                //HttpContent contentPost = new StringContent(JsonSerializer.Serialize(searchRequest), Encoding.UTF8, "application/json");
                //var request = new HttpRequestMessage(HttpMethod.Get, url)
                //{
                //    Content = contentPost
                //};
                AddAuthentication();
                var response = await _customHttpClient.Client.GetAsync(url);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var list = new Page<T>();
                    (Page<T>? values, List<ApiError>? errors, bool isSuccess, bool isFailure)
                        results = BaseApi<T, TValue>.DeserializeToPageList(json);
                    if (results is { isSuccess: true, values: not null })
                    {
                        list = results.values;
                        return list;
                    }
                    if (results.isFailure && results.errors != null && results.errors.Count > 0)
                        throw new ApiErrorException(results.errors);
                }
                await LogError(response);
            }
            catch (Exception ex)
            {
                if (ex is ApiErrorException)
                    throw;
                throw new ApiErrorException($"An error occurred while getting data: {ex.Message}");
            }
            return default!;
        }
        public virtual async Task<T> PostAsync(T model)
        {
            var url = GetUrl($"{BaseUrl}/POST");
            HttpContent contentPost = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            AddAuthentication();
            var response = await _customHttpClient.Client.PostAsync(new Uri(url), contentPost);
            if (response.StatusCode == HttpStatusCode.Created ||
                response.StatusCode == HttpStatusCode.BadRequest ||
                response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(content))
                {
                    var items = BaseApi<T, TValue>.DeserializeJson(content);
                    if (items is { isSuccess: true, values: not null })
                        return items.values[0];
                    if (items.errors != null)
                    {
                        LogError(items.errors);
                        throw new ApiErrorException(items.errors);
                    }
                }
            }
            else
            {
                await LogError(response);
            }
            return default!;
        }
        public virtual async Task<T> PostAsync(T model, string idempotencyKey)
        {
            var url = GetUrl($"{BaseUrl}/POST");
            HttpContent contentPost = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            AddAuthentication();
            if (!string.IsNullOrEmpty(idempotencyKey))
            {
                // Add the Idempotency-Key header
                contentPost.Headers.Add("Idempotency-Key", idempotencyKey);
            }
            var response = await _customHttpClient.Client.PostAsync(new Uri(url), contentPost);
            if (response.StatusCode == HttpStatusCode.Created ||
                response.StatusCode == HttpStatusCode.BadRequest ||
                response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(content))
                {
                    var items = BaseApi<T, TValue>.DeserializeJson(content);
                    if (items is { isSuccess: true, values: not null })
                        return items.values[0];
                    if (items.errors != null)
                    {
                        LogError(items.errors);
                        throw new ApiErrorException(items.errors);
                    }
                }
            }
            else
            {
                await LogError(response);
            }
            return default!;
        }
        public string GenerateIdempotencyKey(T model)
        {
            // Implement your logic to generate a unique idempotency key
            // For example, you can use a hash of the model's JSON representation
            var json = JsonSerializer.Serialize(model);
            return BaseApi<T, TValue>.GenerateHash(json);
        }
        static string GenerateHash(string data)
        {
            var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(data));
            var builder = new StringBuilder();
            foreach (var byteValue in hashedBytes)
            {
                builder.Append(byteValue.ToString("x2"));
            }
            return builder.ToString();
        }
        public virtual async Task<T?> FormPostAsync(IEnumerable<KeyValuePair<string, string>> data, string url)
        {
            try
            {
                url = GetUrl(url);
                HttpContent contentPost = new FormUrlEncodedContent(data);
                //Delete once ok message received from the api
                AddAuthentication();
                var response = await _customHttpClient.Client.PostAsync(new Uri(url), contentPost);
                if (response.StatusCode == HttpStatusCode.Created)// || response.StatusCode == HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var items = BaseApi<T, TValue>.DeserializeJson(content);
                    if (items is { isSuccess: true, values: not null })
                        return items.values[0];
                }
                await LogError(response);
            }
            catch (HttpRequestException n)
            {
                // handle somehow
                throw new ArgumentException(n.Message);
            }
            catch (TimeoutException d)
            {
                // handle somehow
                throw new ArgumentException(d.Message);
            }
            catch (TaskCanceledException g)
            {
                // handle somehow
                throw new ArgumentException(g.Message);
            }
            catch (Exception f)
            {
                throw new ArgumentException(f.ToString());
            }
            throw new ApiErrorException("Error occured");
        }
        public virtual async Task<bool> PutAsync(T data)
        {
            var url = GetUrl($"{BaseUrl}/PUT");
            HttpContent contentPost = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
            AddAuthentication();
            var response = await _customHttpClient.Client.PutAsync(new Uri(url), contentPost);
            if (response.StatusCode == HttpStatusCode.NoContent ||
                response.StatusCode == HttpStatusCode.BadRequest ||
                response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(content))
                {
                    var items = BaseApi<T, TValue>.DeserializeJson(content);
                    if (items is { isSuccess: true, values: not null })
                        return true;
                    if (items.errors != null)
                    {
                        LogError(items.errors);
                        throw new ApiErrorException(items.errors);
                    }
                }
            }
            else
            {
                await LogError(response);
            }
            return false;
        }
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteAsync(TValue id)
        {
            try
            {
                var url = GetUrl($"{BaseUrl}/Delete/{id}");
                if (!string.IsNullOrEmpty(url))
                {
                    AddAuthentication();
                    var response = await _customHttpClient.Client.DeleteAsync(url);
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.NoContent:
                        case HttpStatusCode.OK:
                        case HttpStatusCode.BadRequest:
                            {
                                var content = await response.Content.ReadAsStringAsync();
                                if (!string.IsNullOrEmpty(content))
                                {
                                    var items = BaseApi<T, TValue>.DeserializeJson(content);
                                    if (items is { isSuccess: true, values: not null })
                                        return true;
                                    if (items.errors != null)
                                    {
                                        LogError(items.errors);
                                        throw new ApiErrorException(items.errors);
                                    }
                                }
                            }
                            break;
                        default:
                            {
                                await LogError(response);
                                return false;
                            }
                    }
                }
            }
            catch (HttpRequestException n)
            {
                // handle somehow
                throw new ArgumentException(n.Message);
            }
            catch (TimeoutException d)
            {
                // handle somehow
                throw new ArgumentException(d.Message);
            }
            catch (TaskCanceledException g)
            {
                // handle somehow
                throw new ArgumentException(g.Message);
            }
            catch (Exception f)
            {
                if (f is ApiErrorException)
                    throw;
                throw new ArgumentException(f.Message);
            }
            return false;
        }
        /// <summary>
        /// Complete Url will not call GetUrl 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        /// <exception cref="ApiErrorException"></exception>
        /// <summary>
        /// Generic method for HttpContent with Get Request
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public virtual async Task<HttpResponseMessage> GetHttpContentAsync(string url)
        {
            url = GetUrl(url);
            try
            {
                AddAuthentication();
                return await _customHttpClient.Client.GetAsync(url);
            }
            catch (HttpRequestException n)
            {
                // handle somehow
                throw new ArgumentException(n.Message);
            }
            catch (TimeoutException d)
            {
                // handle somehow
                throw new ArgumentException(d.Message);
            }
            catch (TaskCanceledException g)
            {
                // handle somehow
                throw new ArgumentException(g.Message);
            }
            catch (Exception f)
            {
                throw new ArgumentException(f.ToString());
            }
        }
        public void LogError(List<ApiError> list)
        {
            if (list != null)
            {
                foreach (var error in list)
                {
                    Logger.LogError("{e}",error.Message);
                }
            }
        }
        public virtual async Task LogError(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(content))
                Logger.LogError("{c}", content);
        }
        public string GetUrl(string url)
        {
            if (!url.Contains("http"))
                url = $"{_apiConfiguration.ApiUrl}{url}";
            return url;
        }
        private async void AddAuthentication()
        {
            if (_apiConfiguration.EnableAuthentication)
            {
                await _tokenAuthentication.AddAuthenticationAsync(_customHttpClient);
            }
        }
        internal static (List<T>? values, List<ApiError>? errors, bool isSuccess, bool isFailure) DeserializeJson(string jsonString)
        {
            var rootNode = JsonNode.Parse(jsonString);
            if (rootNode != null)
            {
                var valueNode = rootNode["value"];
                List<T>? values = [];
                switch (valueNode)
                {
                    case JsonArray:
                        values = JsonSerializer.Deserialize<List<T>>(valueNode.ToJsonString());
                        break;
                    case JsonObject:
                        {
                            var singleValue = JsonSerializer.Deserialize<T>(valueNode.ToJsonString());
                            if (singleValue != null)
                            {
                                values.Add(singleValue);
                            }
                            break;
                        }
                }
                bool isSuccess = rootNode["isSuccess"]?.GetValue<bool>() ?? false;
                bool isFailure = rootNode["isFailure"]?.GetValue<bool>() ?? false;
                var errors = JsonSerializer.Deserialize<List<ApiError>>(rootNode["errors"]?.ToJsonString() ?? "[]");
                return (values, errors, isSuccess, isFailure);
            }
            return (null, null, false, false);
        }
        static (Page<T>? values, List<ApiError>? errors, bool isSuccess, bool isFailure) DeserializeToPageList(string jsonString)
        {
            var rootNode = JsonNode.Parse(jsonString);
            if (rootNode != null)
            {
                var valueNode = rootNode["value"];
                Page<T>? values = null;
                if (valueNode != null)
                {
                    values = JsonSerializer.Deserialize<Page<T>>(valueNode.ToJsonString());
                }
                bool isSuccess = rootNode["isSuccess"]?.GetValue<bool>() ?? false;
                bool isFailure = rootNode["isFailure"]?.GetValue<bool>() ?? false;
                var errors = JsonSerializer.Deserialize<List<ApiError>>(rootNode["errors"]?.ToJsonString() ?? "[]");
                return (values, errors, isSuccess, isFailure);
            }
            return (null, null, false, false);
        }
    }
}
