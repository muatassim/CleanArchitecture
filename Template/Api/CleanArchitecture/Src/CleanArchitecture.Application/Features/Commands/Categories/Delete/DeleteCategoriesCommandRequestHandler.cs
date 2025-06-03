using CleanArchitecture.Core.Entities.Errors;
using CleanArchitecture.Core.Abstractions;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Interfaces.Messaging;
using System.Threading.Tasks;
using System.Threading;
namespace CleanArchitecture.Application.Features.Commands.Categories.Delete
{
    public class DeleteCategoriesCommandRequestHandler(ICategoriesDataRepository repository) : ICommandHandler<DeleteCategoriesCommandRequest, bool>
    {
        public async Task<Result<bool>> Handle(DeleteCategoriesCommandRequest request, CancellationToken cancellationToken)
        {
            var isDeleted = await repository.DeleteAsync(request.Id);
            if (!isDeleted)
            {
                return Result.Failure<bool>(CategoriesErrors.NotFound);
            }
            await repository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(isDeleted);
        }
    }
}
