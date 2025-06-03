using CleanArchitecture.ApiShared.Helpers;
using CleanArchitecture.ApiShared.Interfaces;
using CleanArchitecture.ApiShared.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CleanArchitecture.ApiShared.Interfaces.Api;

namespace CleanArchitecture.ApiShared.Api
{
    public class CategoriesModelApi(CustomHttpClient customHttpClient,
        ITokenAuthentication tokenAuthentication,
        IOptions<ApiConfiguration> apiConfiguration,
        ILogger<CategoriesModelApi> logger) : BaseApi<CategoriesModel, int>("/api/Categories", customHttpClient, tokenAuthentication, apiConfiguration, logger), ICategoriesModelApi
    {
        /// <summary>
        /// Put
        /// </summary>
        /// <returns></returns>
        public override async Task<bool> PutAsync(CategoriesModel categoriesModel)
        {
            return await base.PutAsync(categoriesModel);
        }
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="categoriesModelId">CategoriesModelId</param>
        /// <returns>bool</returns>
        public override async Task<bool> DeleteAsync(int categoriesModelId)
        {
            return await base.DeleteAsync(categoriesModelId);
        }
        public override async Task<CategoriesModel> GetAsync(int categoryID)
        {
            return await base.GetAsync(categoryID);
        }
    }
}
