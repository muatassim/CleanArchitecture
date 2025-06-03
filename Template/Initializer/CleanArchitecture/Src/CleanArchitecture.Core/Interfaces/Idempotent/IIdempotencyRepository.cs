namespace CleanArchitecture.Core.Interfaces.Idempotent
{
    /// <summary>
    /// Defines a contract for storing and checking idempotency keys to ensure operations are not repeated.
    /// Typically used to prevent duplicate processing of commands or API requests.
    /// </summary>
    public interface IIdempotencyRepository
    {
        /// <summary>
        /// Checks if the specified idempotency key exists in the repository.
        /// </summary>
        /// <param name="key">The idempotency key to check.</param>
        /// <returns>
        /// A tuple: (Exists, Response) where Exists is true if the key exists, and Response is the associated response data if found.
        /// </returns>
        Task<(bool Exists, string Response)> KeyExistsAsync(string key);

        /// <summary>
        /// Stores the idempotency key and its associated response data in the repository.
        /// </summary>
        /// <param name="key">The idempotency key to store.</param>
        /// <param name="responseData">The response data to associate with the key.</param>
        /// <returns>True if the key was stored successfully; otherwise, false.</returns>
        Task<bool> StoreKeyAsync(string key, string responseData);
    }
}