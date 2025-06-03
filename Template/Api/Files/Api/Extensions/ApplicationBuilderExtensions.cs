using System.Diagnostics; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions; 
using CleanArchitecture.Api.Settings;
using CleanArchitecture.Application;
using CleanArchitecture.Application.Exceptions; 
using CleanArchitecture.Core;
using CleanArchitecture.Infrastructure.MicrosoftSql;
namespace CleanArchitecture.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
         
        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        { 
            //Add App Settings 
            AppSettings? settings = builder.Configuration.GetSection(nameof(AppSettings).ToLower()).Get<AppSettings>()
               ?? throw new InvalidOperationException($"{nameof(AppSettings)} section is missing or invalid.");

            // Add service defaults & Aspire client integrations.
            builder.AddServiceDefaults();
            // Add services to the container.
            builder.Services.AddProblemDetails(options =>
            {
                options.CustomizeProblemDetails = context =>
                {
                    context.ProblemDetails.Instance =
                        $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";
                    context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);
                    Activity? activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
                    context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);
                };
            });
                 
            builder.Services.AddLogging();
            builder.Services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(365);
            }); 
            builder.Services.AddOptions();
            builder.Services.AddSingleton(builder.Configuration);
            builder.Services.AddSingleton(sp => builder.Configuration);
            builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>(); 
            //Add Cache 
            builder.Services.AddMemoryCache();
            AddAppServices(builder.Services, builder.Configuration);
            builder.Services.AddControllers();
            // Configure Scalar, help here:  https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.ConfigureCors();
            if (!string.IsNullOrEmpty(settings.AllowedHostNames))
            {
                builder.Services.Configure<ForwardedHeadersOptions>(options =>
                {
                    options.ForwardedHeaders = ForwardedHeaders.All;
                    options.KnownNetworks.Clear();
                    options.KnownProxies.Clear();
                    options.AllowedHosts = settings.AllowedHosts;
                });
            } 
            builder.Services.AddHealthChecks();
            return builder;
        }
        public static void UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            // Converts unhandled exceptions into Problem Details responses
            app.UseExceptionHandler(new ExceptionHandlerOptions
            {
                StatusCodeSelector = ex => ex switch
                {
                    ArgumentException => StatusCodes.Status400BadRequest,
                    NotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                }
            }); 
            app.UseStatusCodePages();
        }
        public static IServiceCollection ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: Helpers.Constants.MyAllowSpecificOrigins,
                                  policy => ConfigureCorsPolicy(policy));
            });
            return services;
        }
        
        private static void AddAppServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCoreServices(); 
            services.AddInfrastructureMsSql(configuration);
            services.AddApplication();
        }
        private static void ConfigureCorsPolicy(CorsPolicyBuilder policy)
        {
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.AllowAnyOrigin();
            //to Restrict 
            //policy.WithOrigins("http://example.com",
            //                  "http://www.contoso.com");
        }
    }
}
