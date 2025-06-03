using System;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using CleanArchitecture.Infrastructure.MicrosoftSql.Constants; 
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CleanArchitecture.Infrastructure.MicrosoftSqlTests
{
    [TestClass]
    public static class AssemblyStartupClass // Changed to static to comply with MSTEST0016  
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            AppHost.Initialize(null);
            DeleteFiles(KernelMapper.LogPath);
            if (!Directory.Exists(KernelMapper.LogPath))
                Directory.CreateDirectory(KernelMapper.LogPath);
            context.WriteLine("Assembly Init");
        }

        private static void OpenLogFolder()
        {
            if (KernelMapper.OpenLogFolder)
            {
                try
                {
                    var psi = new ProcessStartInfo() { FileName = KernelMapper.LogPath, UseShellExecute = true };
                    Process.Start(psi);
                }
                catch
                {
                    // ignored  
                }
            }
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            OpenLogFolder();
        }

        private static void DeleteFiles(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                foreach (var file in Directory.GetFiles(directoryPath))
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch
                    {
                        // ignore  
                    }
                }
            }
        }
    }
}
