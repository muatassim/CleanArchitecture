using System;
using System.IO;
using CleanArchitecture.Core;
using CleanArchitecture.Infrastructure.MicrosoftSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
namespace CleanArchitecture.Infrastructure.MicrosoftSqlTests
{
    public class AppHost
    {
        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            var hostBuilder = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    ConfigureServices(services);
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
        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddCoreServices();
            services.AddInfrastructureMsSql(Configuration);
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
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
            _host ??= CreateHostBuilder([]).Build();
            _host.StartAsync().GetAwaiter().GetResult();
        }
        public static IServiceProvider GetServiceProvider()
        {
            Initialize(null);
            return _host.Services;
        }
    }
}
