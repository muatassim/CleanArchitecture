using System.Text.Json.Serialization;
namespace CleanArchitecture.ApiShared.Models
{
    public class SearchRequest
    {
        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; } = 10;
        [JsonPropertyName("skip")]
        public int Skip { get; set; } = 0;
        [JsonPropertyName("searchValue")]
        public string SearchValue { get; set; } = string.Empty;
        [JsonPropertyName("sortOrderColumn")]
        public string SortOrderColumn { get; set; } = "id";
        [JsonPropertyName("sortOrder")]
        public OrderBy SortOrder { get; set; } = OrderBy.Ascending;
    }
}
