using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CleanArchitecture.Core.Shared
{
    /// <summary>
    /// Represents a standard structure for search/filter requests, including paging and sorting options.
    /// Commonly used in APIs or repositories to encapsulate user input for paginated, sorted, and filtered queries.
    /// </summary>
    public class SearchRequest
    {
        /// <summary>
        /// The number of items to return per page.
        /// Must be between 5 and 30 (inclusive).
        /// </summary>
        [Required(ErrorMessage = "Page Size is required.")]
        [Range(5, 30, ErrorMessage = "Please enter a value bigger than {5} and Less thank or equal to 30 ")]
        [Description("Page Size")]
        [DefaultValue(5)]
        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// The search/filter value (e.g., a keyword or phrase).
        /// </summary>
        [JsonPropertyName("searchValue")]
        public string SearchValue { get; set; } = string.Empty;

        /// <summary>
        /// The name of the column to sort by.
        /// </summary>
        [Required(ErrorMessage = "Sort Order Column Name is required.")]
        [Description("Sort Order Column")]
        [JsonPropertyName("sortOrderColumn")]
        public string? SortOrderColumn { get; set; }

        /// <summary>
        /// The direction to sort results (ascending or descending).
        /// </summary>
        [Required(ErrorMessage = "Sort Order is required.")]
        [Description("Sort Order")]
        [JsonPropertyName("sortOrder")]
        public OrderBy SortOrder { get; set; } = OrderBy.Ascending;

        /// <summary>
        /// The number of items to skip (for paging).
        /// </summary>
        [JsonPropertyName("skip")]
        public int Skip { get; set; } = 0;
    }
}