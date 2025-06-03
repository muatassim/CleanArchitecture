using CleanArchitecture.ApiShared.Api;
using CleanArchitecture.ApiShared.Helpers;
using CleanArchitecture.ApiShared.Helpers.Handlers;
using CleanArchitecture.ApiShared.Interfaces;
using CleanArchitecture.ApiShared.Models;
using CleanArchitecture.ApiShared.Models.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CleanArchitecture.ApiShared.Interfaces.Api;
namespace CleanArchitecture.ApiShared
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApiHelpers(this IServiceCollection services, IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(services);
            services.AddOptions();
            services.Configure<ApiConfiguration>(configuration.GetSection(nameof(ApiConfiguration)));
            services.Configure<TokenProvider>(configuration.GetSection(nameof(TokenProvider)));
            ApiConfiguration? settings = configuration.GetSection(nameof(ApiConfiguration)).Get<ApiConfiguration>()
                                         ?? throw new InvalidOperationException($"{nameof(ApiConfiguration)} section is missing or invalid.");
            if (!settings.IsValid())
            {
                throw new ArgumentException(settings.ValidateMessage);
            }
            TokenProvider? tokenSettings = configuration.GetSection(nameof(TokenProvider)).Get<TokenProvider>()
                                         ?? throw new InvalidOperationException($"{nameof(TokenProvider)} section is missing or invalid.");
            if (!tokenSettings.IsValid())
            {
                throw new ArgumentException(tokenSettings.ValidateMessage);
            }
            //TokenProvider
            RegisterApi(services, settings);
            return services;
        }
        /// <summary>
        /// Adds authorization services to the specified <see cref="IServiceCollection" />. 
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <param name="model"></param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddApiHelpers(this IServiceCollection services,
            ApiConfiguration model, TokenProvider tokenProvider)
        {
            ArgumentNullException.ThrowIfNull(services);
            services.AddOptions();
            if (!model.IsValid())
            {
                throw new ArgumentException(model.ValidateMessage);
            }
            services.Configure<ApiConfiguration>(options =>
            {
                options.ApiUrl = model.ApiUrl;
                options.MaximumAmountOfRetries = model.MaximumAmountOfRetries;
                options.TimeoutInSeconds = model.TimeoutInSeconds;
                options.EnableAuthentication = model.EnableAuthentication;
            });
            services.Configure<TokenProvider>(options =>
            {
                options.TokenUrl = tokenProvider.TokenUrl;
                options.ClientId = tokenProvider.ClientId;
                options.ClientSecret = tokenProvider.ClientSecret;
                options.Scope = tokenProvider.Scope;
            });
            RegisterApi(services, model);
            return services;
        }
        private static void RegisterApi(IServiceCollection services, ApiConfiguration model)
        {
            services.AddTransient<RetryPolicyDelegatingHandler>(sp =>
                new RetryPolicyDelegatingHandler(model.MaximumAmountOfRetries));
            services.AddTransient<TimeOutDelegatingHandler>(sp =>
                new TimeOutDelegatingHandler(model.TimeoutInSeconds));
            services.AddHttpClient<CustomHttpClient>()
                .AddHttpMessageHandler<TimeOutDelegatingHandler>()
                .AddHttpMessageHandler<RetryPolicyDelegatingHandler>()
                .ConfigurePrimaryHttpMessageHandler(handler =>
                    new HttpClientHandler()
                    {
                        AutomaticDecompression = System.Net.DecompressionMethods.GZip
                    });
            services.AddSingleton<ITokenAuthentication, TokenAuthentication>();
            services.AddScoped<ILookupApiHelper, LookupApi>(); 
            services.AddScoped<ICategoriesModelApi, CategoriesModelApi>(); 
        }
    }
}
