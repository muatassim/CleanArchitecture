using System.Diagnostics;
namespace CleanArchitecture.ApiTests
{
    [TestClass]
    public static class AssemblyStartupClass
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            AppHost.Initialize(null);
            DeleteFiles(KernelMapper.LogPath);
            if (!Directory.Exists(KernelMapper.LogPath))
            {
                Directory.CreateDirectory(KernelMapper.LogPath);
            }
            AssemblyInitializeHelper.AssemblyInitializeAsync().GetAwaiter().GetResult();
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
            AssemblyInitializeHelper.AssemblyCleanup();
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
