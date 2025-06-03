using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using CleanArchitecture.DataInitializer;
namespace CleanArchitecture.Infrastructure.MicrosoftSql.Repositories
{
    public class DatabaseInitializerHostedService(IServiceProvider serviceProvider,
        IConfiguration configuration, ILogger<DatabaseInitializerHostedService> logger) : IHostedService
    {
        private static readonly Lazy<Task> InitializationTask;
        static DatabaseInitializerHostedService()
        {
            InitializationTask = new Lazy<Task>(() => Task.CompletedTask);
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            return InitializeAsync(serviceProvider, configuration, logger, cancellationToken);
        }
        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
        private static async Task InitializeAsync(IServiceProvider serviceProvider, IConfiguration configuration, ILogger logger, CancellationToken cancellationToken)
        {
            if (!InitializationTask.IsValueCreated)
            {
                await InitializationTask.Value;
                using var scope = serviceProvider.CreateScope();
                //var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                //await context.Database.MigrateAsync(cancellationToken);
                await RunInitializerAsync(configuration, logger, cancellationToken);
                //await context.EnsureTablesExistAsync();
            }
        }
        private static async Task RunInitializerAsync(IConfiguration configuration, ILogger logger, CancellationToken cancellationToken)
        {
            logger.LogInformation("Running initialization");
            var connectionString = configuration.GetConnectionString("CleanArchitecture");
            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("Connection string 'CleanArchitecture' is missing or empty.");
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                var dataInitializerOptions = new DataInitializerOptions()
                {
                    ConnectionString = connectionString,
                    DatabaseName = ConnectionStringHelper.GetDatabaseName(connectionString),
                    RecreateDatabase = false,
                    RunSeedData = true,
                    DacPacFile = "CleanArchitectureDb.dacpac"
                };
                await Task.Run(() => Initializer.Run(dataInitializerOptions), cancellationToken);
            }
            logger.LogInformation("End Running initialization");
        }
    }
}
