using System.Text.Json.Serialization;
namespace CleanArchitecture.ApiShared.Models
{
    /// <summary>
    /// Error
    /// </summary>
    public class ApiError(string name, string message)
    {
        [JsonPropertyName("propertyName")]
        public string Name { get; set; } = name;
        [JsonPropertyName("errorMessage")]
        public string Message { get; set; } = message;
        public override string ToString() => $"name: {Name}= message:{Message}";
        public static readonly ApiError NotFound = new(
         "NotFound",
         "The specified Url was not found");
        public static readonly ApiError BadRequest = new(
      "BadRequest",
      "The specified Url return a Bad Request");
        public static readonly ApiError ServerError = new(
      "NotFound",
      "The specified Url specified server Error");
    }
}
