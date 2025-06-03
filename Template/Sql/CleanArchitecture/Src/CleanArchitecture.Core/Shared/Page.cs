using System.Text.Json.Serialization;
namespace CleanArchitecture.Core.Shared
{
    /// <summary>
    /// Stores the filtered, sorted and paginated data for the given entity type.
    /// </summary>
    /// <typeparam name="T">Type of Entity for which pagination is being implemented.</typeparam>
    public class Page<T>
    {
        /// <summary>
        /// IEnumerable of the final sorted, filtered and paginated data.
        /// </summary>
        /// <summary>
        /// IEnumerable of the final sorted, filtered and paginated data.
        /// </summary>
        [JsonPropertyName("results")]
        public IEnumerable<T> Results { get; set; } = [];
        /// <summary>
        /// The current page number.
        /// </summary>
        [JsonPropertyName("currentPage")]
        public int CurrentPage { get; set; }
        /// <summary>
        /// Total number of pages.
        /// </summary>
        [JsonPropertyName("pageCount")]
        public int PageCount { get; set; }
        /// <summary>
        /// Number or records in a page.
        /// </summary>
        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }
        /// <summary>
        /// Total number of records that exist for the query.
        /// </summary>
        [JsonPropertyName("totalRecords")]
        public int TotalRecords { get; set; }
    }
}
