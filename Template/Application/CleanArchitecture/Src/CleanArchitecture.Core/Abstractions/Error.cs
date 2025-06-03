using System.Text.Json.Serialization;

namespace CleanArchitecture.Core.Abstractions
{
    /// <summary>
    /// Represents an error with a name and a message.
    /// Useful for returning or logging error information in a structured way.
    /// </summary>
    public sealed class Error(string name, string message)
    {
        /// <summary>
        /// The name or code of the error (e.g., "ValidationError", "NotFound").
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; init; } = name;

        /// <summary>
        /// The human-readable error message.
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; init; } = message;

        /// <summary>
        /// Returns a string representation of the error.
        /// </summary>
        public override string ToString() => $"name: {Name}= message:{Message}";
    }
}