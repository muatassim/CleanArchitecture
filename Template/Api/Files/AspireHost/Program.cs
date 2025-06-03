using Aspire.Hosting;
using Microsoft.Extensions.Configuration; // Add this namespace for configuration-related methods  
using CleanArchitecture.AppHost;

var builder = DistributedApplication.CreateBuilder(args);
builder.AddForwardedHeaders();
// Add environment variables to the configuration builder
builder.Configuration.AddJsonFile("AppSettings.json", optional: false, reloadOnChange: true)
   .Build(); // Build the configuration 
//var appSettingsSection = builder.Configuration.GetSection("AppSettings");
var apiService =
     builder.AddProject<Projects.CleanArchitecture_Api>(name: Constants.ApiProjectName, options =>
     {
         options.LaunchProfileName = "https";
     })
     .WithEnvironment("RunDbInitializer", builder.Configuration["RunDbInitializer"])
     .WithExternalHttpEndpoints();

builder.AddSqlServerAsync(apiService).Wait();
builder.Build().Run();
