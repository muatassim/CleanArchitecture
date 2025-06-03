using CleanArchitecture.Core.Abstractions;

namespace CleanArchitecture.CoreTest.Data
{
    // Simple test entity
    public class TestEntity(Guid id) : Entity<Guid>(id)
    { 
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        
        public string Value { get; set; } = string.Empty;
    }

}