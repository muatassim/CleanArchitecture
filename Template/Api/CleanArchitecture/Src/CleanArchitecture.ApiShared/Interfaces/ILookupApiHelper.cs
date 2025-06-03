using CleanArchitecture.ApiShared.Models;
namespace CleanArchitecture.ApiShared.Interfaces
{
    public interface ILookupApiHelper
    {
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<IEnumerable<LookUp>> Get(string name);
    }
}
