using CleanArchitecture.ApiShared;
using CleanArchitecture.ApiShared.Models;
using CleanArchitecture.ApiShared.Models.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace CleanArchitecture.ApiTests
{
    public static class TestDependencyInjectionExtensions
    {
        /// <summary>
        /// Add TestApi to the service collection
        /// Use this method to utilize the default configuration values in appSettings 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddTestApi(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddApiHelpers(configuration);
        }
        /// <summary>
        /// Add TestApi to the service collection
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="apiUrl"></param>
        /// <returns></returns>
        public static IServiceCollection AddTestApi(this IServiceCollection services, IConfiguration configuration, string apiUrl)
        {
            ArgumentNullException.ThrowIfNull(services);
            ArgumentNullException.ThrowIfNull(configuration);
            var tokenProvider = GetTokenProvider(configuration);
            var apiConfiguration = GetApiConfiguration(apiUrl,configuration);
            return services.AddApiHelpers(apiConfiguration, tokenProvider);
        }
        static TokenProvider GetTokenProvider(IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(configuration);
            TokenProvider? tokenProvider = configuration.GetSection(nameof(TokenProvider)).Get<TokenProvider>()
                                       ?? throw new InvalidOperationException($"{nameof(TokenProvider)} section is missing or invalid.");
            if (!tokenProvider.IsValid())
            {
                throw new ArgumentException(tokenProvider.ValidateMessage);
            }
            return tokenProvider;
        }
        static ApiConfiguration GetApiConfiguration(string apiUrl, IConfiguration configuration)
        {
            ApiConfiguration? apiConfiguration = configuration.GetSection(nameof(ApiConfiguration)).Get<ApiConfiguration>()
                                    ?? throw new InvalidOperationException($"{nameof(ApiConfiguration)} section is missing or invalid.");
            if (!apiConfiguration.IsValid())
            {
                throw new ArgumentException(apiConfiguration.ValidateMessage);
            }
            apiConfiguration.ApiUrl= apiUrl; 
            return apiConfiguration;
        }
    }
}
