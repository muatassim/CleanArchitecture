using CleanArchitecture.Core.Abstractions;
using CleanArchitecture.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Core
{
    /// <summary>
    /// Provides extension methods for registering core services with the dependency injection container.
    /// This class is intended to centralize and simplify the registration of services, repositories, and other dependencies
    /// required by the CleanArchitecture.Core layer.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers core services into the provided <see cref="IServiceCollection"/>.
        /// Call this method in your application's startup to ensure all core dependencies are available via DI.
        /// </summary>
        /// <param name="services">The service collection to add registrations to.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            // Register your core services here, for example:
            // services.AddScoped<IUnitOfWork, UnitOfWork>();
            // services.AddScoped<IDataRepository<Order, Guid>, OrderRepository>();
            // services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();
            //services.AddSingleton<IIdempotencyKeyGenerator<Employees>, IdempotencyKeyGenerator<Employees>>();
            services.AddSingleton<IDomainEventIdempotencyKeyGenerator, DomainEventIdempotencyKeyGenerator>();
            return services;
        }
    }
}