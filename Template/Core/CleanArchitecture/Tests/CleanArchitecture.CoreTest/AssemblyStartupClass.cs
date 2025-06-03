using System.Diagnostics;
namespace CleanArchitecture.CoreTest
{
    [TestClass]
    public static class AssemblyStartupClass
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            AppHost.Initialize(null);
            DeleteFiles(KernelMapper.LogPath);
            DeleteFiles(KernelMapper.CorePath);
            if (!Directory.Exists(KernelMapper.LogPath))
            {
                Directory.CreateDirectory(KernelMapper.LogPath);
            }
            if (!Directory.Exists(KernelMapper.CorePath))
            {
                Directory.CreateDirectory(KernelMapper.CorePath);
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
