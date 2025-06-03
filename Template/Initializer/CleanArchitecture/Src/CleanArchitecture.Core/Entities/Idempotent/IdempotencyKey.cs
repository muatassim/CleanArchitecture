namespace CleanArchitecture.Core.Entities.Idempotent
{
    /// <summary>
    /// Represents a record for tracking idempotent operations.
    /// Used to ensure that a request with the same key is only processed once.
    /// </summary>
    public class IdempotencyKey
    {
        /// <summary>
        /// The unique identifier for the idempotency key (typically a hash or GUID).
        /// </summary>
        public required string Id { get; set; }

        /// <summary>
        /// The response data associated with this idempotency key.
        /// This allows returning the same response for repeated requests.
        /// </summary>
        public required string ResponseData { get; set; }

        /// <summary>
        /// The date and time when this key was created.
        /// Useful for expiration or cleanup logic.
        /// </summary>
        public DateTime CreatedOn { get; set; }
    }
}