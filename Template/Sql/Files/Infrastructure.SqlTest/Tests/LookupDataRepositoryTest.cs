using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Core.Abstractions;
using CleanArchitecture.Core.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CleanArchitecture.Infrastructure.MicrosoftSqlTests.Tests
{
    [TestClass]
    public class LookupDataRepositoryTest
    {
        static ILookupDataRepository _repository;
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _repository = (ILookupDataRepository)KernelMapper.ServiceProvider.GetService(typeof(ILookupDataRepository));
            context.Write(nameof(LookupDataRepositoryTest));
        }
       
      
        [TestMethod]
        public async Task Get_Dbo_Categories_Lookup_Test()
        {
            IEnumerable<LookUp> list = await _repository.GetDboCategoriesAsync();
            Assert.IsTrue(list != null && list.ToList().Count > 0);
        } 
    }
}
