using System.Diagnostics;
namespace CleanArchitecture.ApplicationTests
{
    [TestClass]
    public class AssemblyStartupClass
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            AppHost.Initialize(null);
            DeleteFiles(KernelMapper.LogPath);
            DeleteFiles(KernelMapper.MapperPath);
            if (!Directory.Exists(KernelMapper.LogPath))
            {
                Directory.CreateDirectory(KernelMapper.LogPath);
            }
            if (!Directory.Exists(KernelMapper.MapperPath))
            {
                Directory.CreateDirectory(KernelMapper.MapperPath);
            }
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
                    //ignored 
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
                        //ignore
                    }
                }
            }
        }
    }
}
