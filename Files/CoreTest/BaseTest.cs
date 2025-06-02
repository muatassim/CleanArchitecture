using CleanArchitecture.Core.Abstractions;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.CoreTest.Data;
namespace CleanArchitecture.CoreTest
{
    public abstract class BaseTest
    {
        public static readonly DateTimeProvider DateTimeProvider;
        static BaseTest()
        {
            DateTimeProvider = new DateTimeProvider(); 
        }
        public static T AssertDomainEventWasPublished<T, TEntityId>(Entity<TEntityId> entity)
            where T : IDomainEvent
        {
            var domainEvent = entity.GetDomainEvents().OfType<T>().SingleOrDefault() ?? throw new Exception($"{typeof(T).Name} was not published");
            return domainEvent;
        }
    }
}
