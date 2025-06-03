using CleanArchitecture.Core.Interfaces.Messaging;

namespace CleanArchitecture.Core.Interfaces
{
    /// <summary>
    /// Marker interface for a cacheable request that returns a response of type <typeparamref name="TResponse"/>.
    /// Inherits from IQuery&lt;TResponse&gt; and ICacheableRequest.
    /// </summary>
    public interface ICacheableRequest<TResponse> : IQuery<TResponse>, ICacheableRequest;

    /// <summary>
    /// Defines the contract for a cacheable request, including cache key and expiration.
    /// </summary>
    public interface ICacheableRequest
    {
        /// <summary>
        /// The unique cache key for this request.
        /// </summary>
        string CacheKey { get; }

        /// <summary>
        /// The cache expiration duration for this request.
        /// </summary>
        TimeSpan Expiration { get; }
    }
}