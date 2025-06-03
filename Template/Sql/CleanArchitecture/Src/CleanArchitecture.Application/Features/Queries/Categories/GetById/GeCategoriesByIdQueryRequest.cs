using CleanArchitecture.Application.Models;
using CleanArchitecture.Core.Interfaces.Messaging;
namespace CleanArchitecture.Application.Features.Queries.Categories.GetById
{
    public class GetCategoriesByIdQueryRequest(int id) : IQuery<CategoriesModel>
    {
        public int Id { get; set; } = id;
    }
}
