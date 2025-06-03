using CleanArchitecture.Core.Abstractions;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Interfaces.Messaging; 
namespace CleanArchitecture.Application.Features.Queries.GetLookUp
{
    public class GetLookUpQueryHandler(ILookupDataRepository repository) : IQueryHandler<GetLookUpQuery, IEnumerable<LookUp>>
    {
        public async Task<Result<IEnumerable<LookUp>>> Handle(GetLookUpQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<LookUp> lookUpCollection = [];
            switch (request.Name.ToLower())
            {
              
                case "dbo_categories":
                    lookUpCollection = await repository.GetDboCategoriesAsync();
                    break; 
            }
            if (!lookUpCollection.Any())
            {
                return Result.Failure<IEnumerable<LookUp>>([new("LookUp.NullValue", "Record is Empty")]);
            }
            return Result.Success(lookUpCollection);
        }
    }
}
