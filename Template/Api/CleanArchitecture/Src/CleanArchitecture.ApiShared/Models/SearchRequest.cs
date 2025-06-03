using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Web;
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
        /// <summary>  
        /// Converts the SearchRequest properties to a query string.  
        /// </summary>  
        public string ToQueryString()
        {
            //https://localhost:7187/api/Categories/Get?PageSize=0&SortOrderColumn=id&SortOrder=Descending&Skip=0
            var query = HttpUtility.ParseQueryString(string.Empty);
            query[nameof(PageSize)] = PageSize.ToString();
            query[nameof(SortOrderColumn)] = SortOrderColumn ?? string.Empty;
            query[nameof(SortOrder)] = SortOrder.ToString();
            query[nameof(Skip)] = Skip.ToString();
            if (!string.IsNullOrEmpty(SearchValue))
            {
                query[nameof(SearchValue)] = SearchValue;
            }
            return query.ToString() ?? string.Empty; // Ensure a non-null return value  
        }
    }
}
