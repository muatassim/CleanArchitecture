using System.ComponentModel;
using System.Text.Json.Serialization;
namespace CleanArchitecture.ApiShared.Models;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OrderBy
{
    [Description("desc")]
    Descending = 1,
    [Description("asc")]
    Ascending = 2
}
