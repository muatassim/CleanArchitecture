using System.Net;
using System.Text.Json.Serialization;
namespace CleanArchitecture.Shared.Models
{
    public class BaseModel
    { 
        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }
        [JsonIgnore]
        public string? ApiMessage { get; set; }
    }
}
