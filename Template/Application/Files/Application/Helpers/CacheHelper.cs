using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
namespace CleanArchitecture.Application.Helpers
{
    public class CacheHelper<TResponse>(IDistributedCache cache)
    {
        private readonly IDistributedCache _cache = cache;
        public async Task<string?> GetCacheRecordAsync(string cacheKey)
        {
            try
            {
                return await _cache.GetStringAsync(cacheKey);
            }
            catch
            {
                // ignored
            }
            return null; // Return null to indicate no data.
        }
        public async Task<TResponse?> GetAsync(string cacheKey)
        {
            try
            {
                byte[]? byteArray = await _cache.GetAsync(cacheKey);
                if (byteArray != null)
                {
                    var jsonObject = Encoding.UTF8.GetString(byteArray);
                    var deSerializeObject = JsonSerializer.Deserialize<TResponse>(jsonObject);
                    return deSerializeObject;
                }
            }
            catch
            {
                // ignored
            }
            return default;
        }
        public async Task<byte[]?> GetCacheAsync(string cacheKey)
        {
            try
            {
                return await _cache.GetAsync(cacheKey);
            }
            catch
            {
                // ignored
            }
            return null;
        }
        public async Task RemoveAsync(string cacheKey)
        {
            try
            {
                await _cache.RemoveAsync(cacheKey);
            }
            catch
            {
                // ignored
            }
        }
        public async Task SetStringAsync(string cacheKey, string jsonString, DistributedCacheEntryOptions options)
        {
            try
            {
                await _cache.SetStringAsync(cacheKey, jsonString, options);
            }
            catch
            {
                // ignored
            }
        }
        public async Task SetAsync(string cacheKey, TResponse response, DistributedCacheEntryOptions options)
        {
            try
            {
                var SerializeObject = JsonSerializer.Serialize(response);
                byte[] byteArray = Encoding.UTF8.GetBytes(SerializeObject);
                await _cache.SetAsync(cacheKey, byteArray, options);
            }
            catch
            {
                // ignored
            }
        }
        public async Task SetAsync(string cacheKey, byte[] data, DistributedCacheEntryOptions options)
        {
            try
            {
                await _cache.SetAsync(cacheKey, data, options);
            }
            catch
            {
                // ignored
            }
        }
    }
}
