using Microsoft.Extensions.Configuration;
namespace CleanArchitecture.ApiTests
{
    public class KernelMapper
    {
        public static IServiceProvider ServiceProvider => AppHost.GetServiceProvider();
        public static string LogPath => Path.Combine(Environment.CurrentDirectory, "Logs");
        public static IConfiguration Configuration => (IConfiguration)(ServiceProvider.GetService(typeof(IConfiguration)) ?? throw new InvalidOperationException("Configuration service not found."));
        public static bool OpenLogFolder
        {
            get
            {
                try
                {
                    if (bool.TryParse(AppHost.Configuration.GetSection("OpenLogFolder").Get<string>(), out var open))
                    {
                        return open;
                    }
                }
                catch
                {
                    //ignore
                }
                return false;
            }
        }
    }
}
