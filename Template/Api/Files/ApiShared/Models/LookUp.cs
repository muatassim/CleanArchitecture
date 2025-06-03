using System.Text.Json.Serialization;
namespace CleanArchitecture.ApiShared.Models
{
    /// <summary>
    /// Declaring Public Class LookUp
    /// </summary>
    public class LookUp
    {
        /// <summary>
        /// Gets or sets the Code
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the Alt
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;
        [JsonPropertyName("group")]
        public string Group { get; set; } = string.Empty;
        public LookUp()
        {
        }
        /// <summary>
        /// OverLoaded Constructor Takes In All Arguments
        /// <param name="code">Code</param>
        /// <param name="description">Alt</param>
        /// </summary>
        public LookUp(string code, string description)
        {
            Code = code;
            Description = description;
        }
        public override string ToString()
        {
            var sb = new System.Text.StringBuilder();
            sb.Append(base.ToString());
            sb.Append(string.Format("Alt: {0}", Description));
            sb.Append(string.Format("Code: {0}", Code));
            return sb.ToString();
        }
    }
}
