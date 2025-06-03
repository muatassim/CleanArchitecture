using System.Text.Json.Serialization;
namespace CleanArchitecture.ApiShared.Models
{
    public class Result<T>
    {
        // Parameterless constructor for deserialization
        public Result() { }
        [JsonPropertyName("value")]
        public List<T>? Value { get; set; }
        [JsonPropertyName("isSuccess")]
        public bool IsSuccess { get; set; }
        [JsonPropertyName("isFailure")]
        public bool IsFailure => !IsSuccess;
        [JsonPropertyName("errors")]
        public List<ApiError>? Errors { get; set; }
    }
}
