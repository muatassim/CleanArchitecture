using CleanArchitecture.Core.Abstractions;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Interfaces.Messaging;
namespace CleanArchitecture.Application.Features.Queries.GetLookUp
{
    public class GetLookUpQuery(string name) : ICacheableRequest<IEnumerable<LookUp>>
    {
        public string Name { get; set; }= name;
        public string CacheKey
        {
            get
            {
                return $"Lookup_{Name}";
            }
        }
        public TimeSpan Expiration => TimeSpan.FromHours(1);
    }
}
