using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Core.Abstractions;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Interfaces.Messaging;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging; 
namespace CleanArchitecture.Application.Behaviors
{
    internal static class CachingDecorator
    {
        internal sealed class QueryHandler<TQuery, TResponse>(
            IQueryHandler<TQuery, TResponse> innerHandler,
            IDistributedCache distributedCache,
            ILogger<QueryHandler<TQuery, TResponse>> logger)
            : IQueryHandler<TQuery, TResponse>
            where TQuery : IQuery<TResponse>
        {
            private readonly CacheHelper<Result<TResponse>> _cacheHelper = new(distributedCache);
            public async Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken)
            {
                var cacheable = GetCacheableRequest(query); 
                if (cacheable != null)
                {
                    var _cacheHelper = new CacheHelper<Result<TResponse>>(distributedCache);
                    var returnValue = await _cacheHelper.GetAsync(cacheable.CacheKey);
                    if (returnValue != null)
                    {
                        logger.LogInformation("Returned value from cache | {name} with key {cacheable.CacheKey}"
                               , typeof(TQuery).Name, cacheable.CacheKey);
                        return (TResponse)Convert.ChangeType(returnValue, typeof(TResponse));
                    }
                    else
                    {
                        Result<TResponse> response = await innerHandler.Handle(query, cancellationToken);
                        await SetCache(cacheable, response);
                        return response;
                    }
                }
                return await innerHandler.Handle(query, cancellationToken);
            }

            #region 
            private static ICacheableRequest? GetCacheableRequest(TQuery request)
            {
                if (request is ICacheableRequest cacheable)
                {
                    if (string.IsNullOrEmpty(cacheable.CacheKey))
                    {
                        return null;
                    }
                    return cacheable;
                }
                return null;
            }
            private async Task SetCache(ICacheableRequest cacheable, Result<TResponse> response)
            {
                try
                {
                    DistributedCacheEntryOptions options = new();
                    options.SetSlidingExpiration(cacheable.Expiration);
                    await _cacheHelper.SetAsync(cacheable.CacheKey, response, options);
                    logger.LogInformation("Added to cache | {typeof(ICacheableRequest).Name} with key {cacheable.CacheKey}",
                           typeof(ICacheableRequest).Name, cacheable.CacheKey);
                }
                catch (Exception f)
                {
                    logger.LogError("Error Setting Cache | {Name} : on {CacheKey} : Error Details:  {Message}",
                        typeof(ICacheableRequest).Name, cacheable.CacheKey, f.Message);
                }
            }
            #endregion
        }
    }
}
