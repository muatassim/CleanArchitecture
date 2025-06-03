using Aspire.Hosting.Lifecycle;
using Docker.DotNet;
using Docker.DotNet.Models;
namespace CleanArchitecture.AppHost;
internal static class Extensions
{
    /// <summary>
    /// Adds a hook to set the ASPNETCORE_FORWARDEDHEADERS_ENABLED environment variable to true for all projects in the application.
    /// </summary>
    public static IDistributedApplicationBuilder AddForwardedHeaders(this IDistributedApplicationBuilder builder)
    {
        builder.Services.TryAddLifecycleHook<AddForwardHeadersHook>();
        return builder;
    }
    public class AddForwardHeadersHook : IDistributedApplicationLifecycleHook
    {
        public Task BeforeStartAsync(DistributedApplicationModel appModel, CancellationToken cancellationToken = default)
        {
            foreach (var p in appModel.GetProjectResources())
            {
                p.Annotations.Add(new EnvironmentCallbackAnnotation(context =>
                {
                    context.EnvironmentVariables["ASPNETCORE_FORWARDEDHEADERS_ENABLED"] = "true";
                }));
            }
            return Task.CompletedTask;
        }
    }
    /// <summary>
    /// Configures  projects to use Ollama for text embedding and chat.
    /// </summary>
    public static async Task<IDistributedApplicationBuilder> AddSqlServerAsync(this IDistributedApplicationBuilder builder,
        IResourceBuilder<ProjectResource> apiService)
    {
        var password = builder.AddParameter("SqlServerSaPassword", secret: false);
        var sqlServerName = $"{Environment.MachineName.ToLower()}server";
        var sqlServerPort = 26364;
        var dockerClient = new DockerClientConfiguration(new Uri("npipe://./pipe/docker_engine")).CreateClient();
        var containers = await dockerClient.Containers.ListContainersAsync(new ContainersListParameters() { All = true });
        var sqlServerContainer = containers.FirstOrDefault(c => c.Names.Contains($"/{sqlServerName}"));
        foreach (var container in containers)
        {
            foreach (var name in container.Names)
            {
                if (name.Contains(sqlServerName))
                {
                    sqlServerContainer = container;
                    break;
                }
            }
        }
        if (sqlServerContainer == null)
        {
            // Create the SQL Server container if it does not exist
            var sqlServer = builder.AddSqlServer(sqlServerName, password: password, port: sqlServerPort)
                .WithLifetime(ContainerLifetime.Persistent);
            // Add the NorthwindTest database
            var sqlDb = sqlServer.AddDatabase("CleanArchitecture", "master");
            apiService
                .WithReference(sqlDb)
                .WaitFor(sqlServer)
                .WaitFor(sqlDb);
        }
        else
        {
            var containerName = sqlServerContainer.Names.First(name => name.Contains(sqlServerName)).TrimStart('/');
            // Use the existing SQL Server container
            var sqlServer = builder.AddSqlServer(sqlServerName, password: password, port: sqlServerPort)
                .WithContainerName(containerName)
                .WithLifetime(ContainerLifetime.Persistent);
            // Add the NorthwindTest database
            var sqlDb = sqlServer.AddDatabase("CleanArchitecture", "master");
            apiService
                .WithReference(sqlDb)
                .WaitFor(sqlServer)
                .WaitFor(sqlDb);
        }
        return builder;
    }
}
