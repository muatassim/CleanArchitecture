namespace CleanArchitecture.Core.Entities.Outbox
{
    /// <summary>
    /// Represents a message stored in the outbox as part of the Outbox pattern.
    /// Used to reliably persist and later dispatch integration events or messages.
    /// </summary>
    public sealed class OutboxMessage
    {
        /// <summary>
        /// Unique identifier for the outbox message.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// The UTC timestamp when the event occurred.
        /// </summary>
        public DateTime OccurredOnUtc { get; set; }

        /// <summary>
        /// The type name of the event (for deserialization or processing).
        /// </summary>
        public required string EventType { get; set; }

        /// <summary>
        /// The serialized content of the event or message.
        /// </summary>
        public required string Content { get; set; }

        /// <summary>
        /// The UTC timestamp when the message was processed (null if not yet processed).
        /// </summary>
        public DateTime? ProcessedOnUtc { get; set; }

        /// <summary>
        /// Error message if processing failed (null if successful or not attempted).
        /// </summary>
        public string? Error { get; set; }

        /// <summary>
        /// Idempotency key to ensure the message is processed only once.
        /// </summary>
        public required string IdempotencyKey { get; set; }
    }
}