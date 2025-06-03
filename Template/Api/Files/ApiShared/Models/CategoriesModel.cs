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
            Picture =  new byte[0];
            ProductsCategoryIDList = []; 
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
        /// <summary>
        /// Picture  
        /// </summary>
        [JsonPropertyName("picture")]
        public byte[] Picture {get; set;} = new byte[0];
        /// <summary>
        /// CategoryID Objects
        /// </summary>
        [JsonPropertyName("productsCategoryIDList")]
        public List<ProductsModel> ProductsCategoryIDList {get; set;} =[];
        /// <summary>
        /// Add Child CategoryIDList Objects
        /// </summary>
        /// <param name="products"></param>
        public void AddProductsCategoryID(ProductsModel products)
        {
            ProductsCategoryIDList.Add(products);
        }
    }
}
