using Microsoft.Extensions.DependencyInjection;
namespace CleanArchitecture.ApiTests
{
    [TestClass]
    public abstract class BaseTest
    {
        // Ensure ServiceProvider is initialized before use  
        protected static ServiceProvider ServiceProvider => AssemblyInitializeHelper.ServiceProvider;

        [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void ClassInitialize(TestContext context)
        {
            context.WriteLine($"{context.FullyQualifiedTestClassName} is initiated!");
        }

        [ClassCleanup(InheritanceBehavior.BeforeEachDerivedClass, ClassCleanupBehavior.EndOfClass)]
        public static void Cleanup()
        {
            // Cleanup resources  
        }

        // Fix for MSTEST0032: Remove the assertion as its condition is always true  
        [TestMethod]
        public void DummyTestMethod()
        {
            // Removed Assert.IsTrue(true) as it is always true and unnecessary  
        }
    }
}
