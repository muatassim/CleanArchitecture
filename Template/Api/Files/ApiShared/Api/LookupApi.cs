using System.Net;
using CleanArchitecture.ApiShared.Exceptions;
using CleanArchitecture.ApiShared.Helpers;
using CleanArchitecture.ApiShared.Interfaces;
using CleanArchitecture.ApiShared.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
namespace CleanArchitecture.ApiShared.Api
{
    public class LookupApi(
        CustomHttpClient customHttpClient,
        ITokenAuthentication tokenAuthentication,
        IOptions<ApiConfiguration> apiConfiguration,
        ILogger<LookupApi> logger)
        : BaseApi<LookUp, string>("/api/lookup", customHttpClient, tokenAuthentication, apiConfiguration, logger),
            ILookupApiHelper
    {
        public async Task<IEnumerable<LookUp>> Get(string name)
        {
            var response = await GetHttpContentAsync(url: $"{BaseUrl}/Get/{name}");
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var items = DeserializeJson(responseContent);
                        if (items is { isSuccess: true, values: not null })
                        {
                            return items.values;
                        }
                        if (items.errors != null)
                        {
                            await LogError(response);
                            throw new ApiErrorException(items.errors);
                        }
                        break;
                    }
            }
            return [];
        }
    }
}
