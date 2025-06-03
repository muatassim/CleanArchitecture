using CleanArchitecture.Core.Interfaces;
namespace CleanArchitecture.Infrastructure.MicrosoftSqlTests.Data
{
    public class TestEvent(string email, int iterationCount) : IDomainEvent
    {
        public string Email { get; } = email;
        public int IterationCount { get; } = iterationCount;
    }
}
