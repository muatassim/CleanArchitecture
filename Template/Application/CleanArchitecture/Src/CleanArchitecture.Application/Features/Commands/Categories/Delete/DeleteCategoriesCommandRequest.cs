using CleanArchitecture.Core.Interfaces.Messaging;
namespace CleanArchitecture.Application.Features.Commands.Categories.Delete
{
    public class DeleteCategoriesCommandRequest(int id) : ICommand<bool>
    {
        public int Id { get; set; } = id;
    }
}
