using Aspire.Hosting.ApplicationModel;
using Aspire.Hosting.Testing;
using CleanArchitecture.AppHost;
using Microsoft.Extensions.DependencyInjection;
namespace CleanArchitecture.ApiTests
{
    public abstract class AssemblyInitializeHelper
    {
        private static ServiceProvider? _serviceProvider;
        private static Aspire.Hosting.DistributedApplication? _hostingApp;
        // Ensure ServiceProvider is initialized before use
        public static ServiceProvider ServiceProvider => _serviceProvider ?? throw new InvalidOperationException("ServiceProvider is not initialized.");
        public static async Task AssemblyInitializeAsync()
        {
            var services = new ServiceCollection();
            // Arrange
            var appHost = await DistributedApplicationTestingBuilder.CreateAsync<Projects.CleanArchitecture_Api>();
            appHost.Services.ConfigureHttpClientDefaults(builder =>
            {
                builder.AddStandardResilienceHandler();
            });
            var app = await appHost.BuildAsync();
            var resourceNotification = app.Services.GetRequiredService<ResourceNotificationService>();
            await app.StartAsync();
            var httpClient = app.CreateHttpClient(Constants.ApiProjectName);
            await resourceNotification.WaitForResourceAsync(Constants.ApiProjectName, KnownResourceStates.Running);
            if (httpClient.BaseAddress == null)
            {
                throw new InvalidOperationException("Base address is null");
            }

            // Ensure the base address uses HTTPS
            if (!string.Equals(httpClient.BaseAddress.Scheme, "https", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException($"API is not running on HTTPS. Current scheme: {httpClient.BaseAddress.Scheme}");
            }
            // Get the URL where the API is running under Aspire
            var apiUrl = httpClient.BaseAddress.AbsoluteUri.ToString();
            apiUrl = apiUrl[..^1]; // remove the trailing slash 
            // Register other necessary services
            services.AddTestApi(KernelMapper.Configuration, apiUrl);
            _serviceProvider = services.BuildServiceProvider();
            _hostingApp = app;
        }
        public static void AssemblyCleanup()
        {
            // Cleanup resources
            _hostingApp?.Dispose();
            _serviceProvider = null;
        } 
    }
}
