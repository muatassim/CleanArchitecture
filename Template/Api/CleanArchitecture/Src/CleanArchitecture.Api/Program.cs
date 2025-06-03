using CleanArchitecture.Api.Extensions;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureServices();
WebApplication app = builder.Build();
app.AddWebApplication(builder.Environment);