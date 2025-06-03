using CleanArchitecture.Core.Entities;

namespace CleanArchitecture.Core.Entities.Builders
{
    public class CategoriesBuilder
    {
        /// <summary>
        /// CategoryID  
        /// </summary>
        public int Id {get; private set;} 
        /// <summary>
        /// CategoryName  
        /// </summary>
        public string CategoryName {get; private set;} = "";
        /// <summary>
        /// Description  
        /// </summary>
        public string Description {get; private set;} = ""; 
        /// <summary>
        /// Set Id  
        /// <param name="id"></param>
        /// </summary>
        public CategoriesBuilder SetId(int id)
        {
            Id = id;
            return this;
        }
        /// <summary>
        /// Set CategoryName  
        /// <param name="categoryName"></param>
        /// </summary>
        public CategoriesBuilder SetCategoryName(string categoryName)
        {
            CategoryName = categoryName;
            return this;
        }
        /// <summary>
        /// Set Description  
        /// <param name="description"></param>
        /// </summary>
        public CategoriesBuilder SetDescription(string description)
        {
            Description = description;
            return this;
        }
     
    
    
        public Categories Build()
        {
            var categories = Categories.InternalCreate(Id,CategoryName,Description);
         
            return categories; 
        }
        public Categories BuildPartial(Categories existingCategories)
        {
            var categories = Categories.InternalCreate(
                id: existingCategories.Id,
                categoryName: string.IsNullOrEmpty(CategoryName) ? existingCategories.CategoryName : CategoryName,
                description: string.IsNullOrEmpty(Description) ? existingCategories.Description : Description);
       
            return categories; 
        }
    }
}
