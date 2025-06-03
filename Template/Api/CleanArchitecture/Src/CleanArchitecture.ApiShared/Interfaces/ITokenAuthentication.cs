using CleanArchitecture.ApiShared.Helpers;
using CleanArchitecture.ApiShared.Models;
namespace CleanArchitecture.ApiShared.Interfaces
{
    public interface ITokenAuthentication
    {
        /// <summary>
        /// Get Access Token 
        /// </summary>
        /// <param name="refresh"></param>
        /// <returns></returns>
        Task<AccessToken> GetAccessTokenAsync(bool refresh = false);
        /// <summary>
        /// Add Authentication
        /// </summary>
        /// <param name="customHttpClient"></param>
        /// <returns></returns>
        Task AddAuthenticationAsync(CustomHttpClient customHttpClient);
    }
}
