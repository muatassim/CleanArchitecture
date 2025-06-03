using CleanArchitecture.Shared.Interfaces; 
using Microsoft.Extensions.DependencyInjection;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Core.Entities;
namespace CleanArchitecture.Application.Mappers
{
    public static class MapperExtensions
    {
        public static IServiceCollection AddMappers(this IServiceCollection services)
        { 
            services.AddScoped<IMapper<CategoriesModel,Categories, int>, CategoriesMapper>();
            services.AddScoped<CategoriesMapper>(); 
            return services;
        }
    }
}
