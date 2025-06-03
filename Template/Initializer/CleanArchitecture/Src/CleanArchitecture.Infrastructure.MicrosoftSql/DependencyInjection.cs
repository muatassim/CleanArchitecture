using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Infrastructure.MicrosoftSql.Clock;
using CleanArchitecture.Infrastructure.MicrosoftSql.Interceptors;
using CleanArchitecture.Infrastructure.MicrosoftSql.Repositories;
using CleanArchitecture.Infrastructure.MicrosoftSql.Repositories.Specifications;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
namespace CleanArchitecture.Infrastructure.MicrosoftSql
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureMsSql(this IServiceCollection services,
              IConfiguration configuration)
        {
            ConfigureServices(services, configuration);
            return services;
        }
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var serviceProvider = services.BuildServiceProvider();
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            var connectionString = GetConnectionString(configuration);
            services.AddDbContextFactory<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 10,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
                });
#if DEBUG
                options.EnableDetailedErrors(true);
                options.EnableSensitiveDataLogging(true);
                options.AddInterceptors(new DatabaseLongQueryLoggerInterceptor(loggerFactory.CreateLogger<DatabaseLongQueryLoggerInterceptor>()));
                options.AddInterceptors(new LoggingSavingChangesInterceptor(loggerFactory.CreateLogger<LoggingSavingChangesInterceptor>()));
#endif
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddScoped<ICategoriesDataRepository, CategoriesDataRepository>();
            services.AddScoped<IDataRepository<Categories, Int32>, CategoriesDataRepository>();
            services.AddScoped<ICategoriesSearchSpecificationFactory, CategoriesSearchSpecificationFactory>();
            services.AddScoped<ILookupDataRepository, LookupDataRepository>();
            //Database Initializer Code 
            var runDbInitializer = configuration.GetSection("RunDbInitializer").Get<bool>();
            if (runDbInitializer)
                services.AddSingleton<IHostedService, DatabaseInitializerHostedService>();
        }
        private static string GetConnectionString(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("CleanArchitecture");
            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("Connection string 'CleanArchitecture' is missing or empty.");
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                var builder = new SqlConnectionStringBuilder(connectionString);
                // Check if the initial catalog (database) is "master" and update it
                if (builder.InitialCatalog == "master")
                {
                    builder.InitialCatalog = "CleanArchitecture";
                }
                connectionString = builder.ConnectionString;
            }
            return connectionString;
        }
    }
}
