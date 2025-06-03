using CleanArchitecture.Application.Behaviors;
using CleanArchitecture.Application.Features.Commands.Categories.Add;
using Microsoft.Extensions.DependencyInjection; 
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Interfaces.Messaging;
using CleanArchitecture.Application.Features.Queries.Categories.GetById;
using CleanArchitecture.Application.Features.Validators.Categories;
using CleanArchitecture.Application.Features.Notifications.Categories;
namespace CleanArchitecture.Application.Features
{
    public static class Extensions
    {
       
        public static IServiceCollection AddBehaviors(this IServiceCollection services, bool useCache)
        { 
            services.Decorate(typeof(IQueryHandler<,>), typeof(LoggingDecorator.QueryHandler<,>));
            services.Decorate(typeof(ICommandHandler<,>), typeof(ValidationDecorator.CommandHandler<,>));

            if (useCache)
            {
                services.Decorate(typeof(IQueryHandler<,>), typeof(CachingDecorator.QueryHandler<,>));
             } 
            services.Decorate(typeof(ICommandHandler<,>), typeof(IdempotentCommandDecorator.CommandHandler<,>)); 
            return services;
        }


        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.Scan(scan =>
            {
            // Scan for ICommandHandler<,> in all relevant assemblies
            scan.FromAssemblies(
                    typeof(AddCategoriesCommandRequestHandler).Assembly,
                    typeof(GetCategoriesByIdQueryRequest).Assembly)
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)), publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime();

            // Scan for ICommandHandler<> if you use it
            scan.FromAssemblies(
                    typeof(AddCategoriesCommandRequestHandler).Assembly)
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)), publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime();

            // Scan for IQueryHandler<,>
            scan.FromAssemblies(
                    typeof(AddCategoriesCommandRequestHandler).Assembly)
                .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)), publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime(); 

            });
            return services;
        }
        public static IServiceCollection AddValidators(this IServiceCollection services )
        {
            services.Scan(scan =>
            {
                // Scan for IValidationHandler handlers in the same assembly as AddEmployeesCommandRequestValidator
                scan.FromAssemblyOf<AddCategoriesCommandRequestValidator>()
               .AddClasses(classes => classes.AssignableTo<IValidationHandler>())
                 .AsImplementedInterfaces()
                 .WithTransientLifetime();
            });
            return services;
        }

        public static IServiceCollection AddDomainEvents(this IServiceCollection services)
        {
            services.Scan(static scan =>
            {
                // Scan for IDomainEventHandler handlers in the same assembly as CategoriesCreatedEventNotificationHandler
                scan.FromAssemblyOf<CategoriesCreatedEventNotificationHandler>()
                  .AddClasses(classes => classes.AssignableTo(typeof(IDomainEventHandler<>)), publicOnly: false) //pick one
                    .AsImplementedInterfaces()
                    .WithScopedLifetime();
            });
            return services;
        }
    }
}
