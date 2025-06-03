using CleanArchitecture.Shared.Models;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
namespace CleanArchitecture.Application.Models
{
    public class CategoriesModel : BaseModel
    {
        
        public CategoriesModel()
        {
            Id = 0;
        }
        /// <summary>
        /// Id 
        /// </summary>
        [JsonPropertyName("id")]
        [DataMember(Name = nameof(Id), IsRequired = true)]
        [Required(ErrorMessage = $"{nameof(Id)} is required")]
        [Display(Name = nameof(Id))]
        public int Id { get; set; }
        /// <summary>
        /// CategoryName  
        /// </summary>
        [JsonPropertyName("categoryName")]
        [StringLength(15, ErrorMessage= $"{nameof(CategoryName)} cannot be greater than 15")]
        [DataMember(Name = nameof(CategoryName),IsRequired = true)]
        [Required(ErrorMessage =  $"{nameof(CategoryName)} is required")]
        [Display(Name = nameof(CategoryName))]
        public string CategoryName {get; set;} = "";
        /// <summary>
        /// Description  
        /// </summary>
        [JsonPropertyName("description")]
        [StringLength(8, ErrorMessage= $"{nameof(Description)} cannot be greater than 8")]
        [DataMember(Name = nameof(Description),IsRequired = false)]
        [Display(Name = nameof(Description))]
        public string Description {get; set;} = "";
    }
}
