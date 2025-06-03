using CleanArchitecture.Core.Interfaces;
namespace CleanArchitecture.ApplicationTests.Data
{
    public class TestEvent(string email, int iterationCount) : IDomainEvent
    {
        public string Email { get; } = email;
        public int iterationCount { get; } = iterationCount;
    }
}
