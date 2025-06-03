using CleanArchitecture.Application.Exceptions;
using Scalar.AspNetCore;
namespace CleanArchitecture.Api.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddWebApplication(this WebApplication app, IWebHostEnvironment environment)
        {
            app.UseForwardedHeaders();
            app.UseHsts();
            app.MapOpenApi();
            // Configure the HTTP request pipeline.
            if (environment.IsDevelopment())
            { 
                app.UseScalarEndpoints(); 
                // Add this line to redirect root to /Scalar/v1
                app.MapGet("/", () => Results.Redirect("/Scalar/v1"));

            }
            app.UseHttpsRedirection(); 
            app.UseCertificateForwarding(); 
            app.UseCustomExceptionHandler(); 
            app.UseRouting();
            //No Default Policy use EnableCors Attribute to all specific policies
            app.UseCors(Helpers.Constants.MyAllowSpecificOrigins); 
            app.MapControllers();
            app.Run();
        }
        public static WebApplication UseScalarEndpoints(this WebApplication app)
        {
            app.MapScalarApiReference(options =>
            {
                // Fluent API
                options
                    .WithTitle("CleanArchitecture")
                    .WithSidebar(true)
                    .WithDarkMode(true)
                    .WithDarkModeToggle(true)
                    .WithDefaultFonts(true);  
            });
            return app;
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

    }
}
