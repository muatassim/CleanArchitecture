using CleanArchitecture.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
namespace CleanArchitecture.ApplicationTests
{
    public class AppHost
    {
        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            var hostBuilder = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddApplication();
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                    var fileName = $"{DateTime.Now:HHmmss}";
                    logging.AddFile($"Logs/Ui-{DateTime.Now:yyyyMMdd}-{fileName}.txt", LogLevel.Information);
                });
            Configuration = InitializeConfiguration();
            return hostBuilder;
        }
        private static IConfiguration InitializeConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }
        private static IHost _host;
        public static IConfiguration Configuration { get; set; }
        public static void Initialize(string[] args)
        {
            if (args != null)
                Console.WriteLine(args);
            _host ??= CreateHostBuilder([]).Build();
        }
        public static IServiceProvider GetServiceProvider()
        {
            Initialize(null);
            return _host.Services;
        }
    }
}
