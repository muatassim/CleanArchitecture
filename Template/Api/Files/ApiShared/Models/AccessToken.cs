using System.Text.Json.Serialization;
namespace CleanArchitecture.ApiShared.Models
{
    public class AccessToken
    {
        /// <summary>
        /// Token 
        /// </summary>
        [JsonPropertyName("access_token")]
        public required string Token { get; set; }
        /// <summary>
        /// Token Type 
        /// </summary>
        [JsonPropertyName("token_type")]
        public required string TokenType { get; set; }
        /// <summary>
        /// Scope 
        /// </summary>
        [JsonPropertyName("scope")]
        public required string Scope { get; set; }
        /// <summary>
        /// Expire Date Time 
        /// By default add Access Token Expire Time Span in Current Time 
        /// </summary>
        [JsonIgnore]
        public DateTime ExpireDateTime
        {
            get
            {
                if (ExpiresIn > 0)
                    return DateTime.UtcNow + TimeSpan.FromSeconds(ExpiresIn);
                return DateTime.UtcNow;
            }
        }
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
    }
}
