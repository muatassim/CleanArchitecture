using Microsoft.Extensions.Configuration;
using System.Reflection;
namespace CleanArchitecture.CoreTest
{
    public static class KernelMapper
    {
        public static IServiceProvider ServiceProvider => AppHost.GetServiceProvider();
        public static string LogPath => Path.Combine(Environment.CurrentDirectory, "Logs");
        public static string CorePath
        {
            get
            {
                return Path.Combine(LogPath, Assembly.GetExecutingAssembly().GetName().Name);
            }
        }
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
