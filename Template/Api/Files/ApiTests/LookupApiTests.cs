using CleanArchitecture.ApiShared.Interfaces;
using Microsoft.Extensions.Logging;
namespace CleanArchitecture.ApiTests
{
    [TestClass]
    public class LookupApiTests : BaseTest
    {
        static ILogger<LookupApiTests>? _logger;
        static ILookupApiHelper? lookupHelper;
        [TestInitialize]
        public void TestInitialize()
        {
            _logger = ServiceProvider.GetService(typeof(ILogger<LookupApiTests>)) as ILogger<LookupApiTests>;
            _logger?.LogInformation("TestInitialize");
            lookupHelper = ServiceProvider.GetService(typeof(ILookupApiHelper)) as ILookupApiHelper;
        }
        [DataTestMethod] 
        [DataRow("dbo_categories")] 
        public async Task GetLookupApiTests(string name)
        {
            if (lookupHelper == null)
            {
                Assert.Fail($"Service {nameof(ILookupApiHelper)} is not initialized.");
            }
            var lookUpCollection = await lookupHelper.Get(name);
            Assert.IsTrue(lookUpCollection != null && lookUpCollection.Any());
        }
    }
}
