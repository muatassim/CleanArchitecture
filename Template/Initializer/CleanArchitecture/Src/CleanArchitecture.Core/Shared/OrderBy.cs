using System.ComponentModel;
using System.Text.Json.Serialization;

namespace CleanArchitecture.Core.Shared
{
    /// <summary>
    /// Specifies the direction for sorting operations.
    /// Used to indicate whether results should be ordered ascending or descending.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OrderBy
    {
        /// <summary>
        /// Sort in descending order ("desc").
        /// </summary>
        [Description("desc")]
        Descending = 1,

        /// <summary>
        /// Sort in ascending order ("asc").
        /// </summary>
        [Description("asc")]
        Ascending = 2
    }
}