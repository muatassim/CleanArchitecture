using CleanArchitecture.ApiShared.Models;
namespace CleanArchitecture.ApiShared.Interfaces.Api
{
    public interface IApi<T, in TValue>
    {
        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        Task<Page<T>> GetAsync(SearchRequest searchParameters);
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id">TValue</param>
        /// <returns></returns>
        Task<T> GetAsync(TValue id);
        /// <summary>
        /// GenerateIdempotencyKey
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        string GenerateIdempotencyKey(T model);
        /// <summary>
        /// Post
        /// </summary>
        /// <returns>Client</returns>
        Task<T> PostAsync(T model,string idempotencyKey); 
        /// <summary>
        /// Post
        /// </summary>
        /// <returns>Client</returns>
        Task<T> PostAsync(T model); 
        /// <summary>
        /// Put
        /// </summary>
        /// <returns></returns>
        Task<bool> PutAsync(T model);
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>bool</returns>
        Task<bool> DeleteAsync(TValue id);
    }
}
