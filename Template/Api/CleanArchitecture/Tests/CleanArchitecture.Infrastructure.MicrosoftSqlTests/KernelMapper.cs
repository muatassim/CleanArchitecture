using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
namespace CleanArchitecture.Infrastructure.MicrosoftSqlTests
{
    public static class KernelMapper
    {
        public static IServiceProvider ServiceProvider => AppHost.GetServiceProvider();
        public static IConfiguration Configuration => (IConfiguration)ServiceProvider.GetService
                 (typeof(IConfiguration));
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
