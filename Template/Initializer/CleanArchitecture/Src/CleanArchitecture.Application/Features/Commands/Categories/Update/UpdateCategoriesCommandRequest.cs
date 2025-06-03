using CleanArchitecture.Application.Models;
using CleanArchitecture.Core.Interfaces.Messaging;
namespace CleanArchitecture.Application.Features.Commands.Categories.Update
{
    public class UpdateCategoriesCommandRequest(CategoriesModel categories) : ICommand<bool>
    {
        public CategoriesModel Categories { get; private set; } = categories;
    }
}
