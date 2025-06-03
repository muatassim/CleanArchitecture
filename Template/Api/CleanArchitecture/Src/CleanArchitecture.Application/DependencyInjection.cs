using CleanArchitecture.Shared.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Application.Features;
using CleanArchitecture.Application.Mappers;
using CleanArchitecture.Application.Helpers;
namespace CleanArchitecture.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.TryAddSingleton<IClaimsHelper, ClaimHelper>();
            services.AddMappers();
            //Cache  
            services.AddDistributedMemoryCache();
            //Mediatr
            services.AddScoped<IDispatcher, Dispatcher>();
            services.AddHandlers();
            services.AddValidators(); 
            services.AddDomainEvents(); 
            services.AddBehaviors(true); 
            return services;
        }
    }
}
