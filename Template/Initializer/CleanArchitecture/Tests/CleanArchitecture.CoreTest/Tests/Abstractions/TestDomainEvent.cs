using CleanArchitecture.Core.Interfaces;

namespace CleanArchitecture.CoreTest.Tests.Abstractions
{
    // Simple test domain event
    public class TestDomainEvent : IDomainEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime OccurredOn { get; set; }
    }
}