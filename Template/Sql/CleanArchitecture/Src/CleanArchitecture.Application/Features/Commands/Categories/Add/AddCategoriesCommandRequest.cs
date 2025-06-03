using CleanArchitecture.Application.Models;
using CleanArchitecture.Core.Interfaces.Messaging;
namespace CleanArchitecture.Application.Features.Commands.Categories.Add
{
    public class AddCategoriesCommandRequest(CategoriesModel categories) : ICommand<CategoriesModel>
    {
        public CategoriesModel Categories { get; private set; } = categories;
    }
}
