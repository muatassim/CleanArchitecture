using System.Text.Json.Serialization;
namespace CleanArchitecture.ApiShared.Models
{
    public class CategoriesModel
    {
        public CategoriesModel()
        {
            Id =  0;
            CategoryName =  "";
            Description =  ""; 
        }
        /// <summary>
        /// Id  
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }
        /// <summary>
        /// CategoryName  
        /// </summary>
        [JsonPropertyName("categoryName")]
        public string CategoryName {get; set;} = "";
        /// <summary>
        /// Description  
        /// </summary>
        [JsonPropertyName("description")]
        public string Description {get; set;} = ""; 
    }
}
