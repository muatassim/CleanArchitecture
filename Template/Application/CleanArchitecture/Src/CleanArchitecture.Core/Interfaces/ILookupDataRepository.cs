using CleanArchitecture.Core.Abstractions;

namespace CleanArchitecture.Core.Interfaces
{
    public interface ILookupDataRepository
    {
        /// <summary>
        /// Sample interface  List
        /// </summary>
        /// <returns>LookUp List</returns>
        Task<IEnumerable<LookUp>> GetDboCategoriesAsync(); 
    }
}
