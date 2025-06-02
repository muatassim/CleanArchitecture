namespace CleanArchitecture.Core.Interfaces
{
    /// <summary>
    /// In Domain-Driven Design (DDD), the concept of an IAggregateRoot interface is used to mark aggregate root entities.
    /// 
    /// Benefits of defining IAggregateRoot for entities:
    /// 1. Clear Intent: Indicates which entities are aggregate roots, clarifying the domain model structure.
    /// 2. Consistency: Provides a consistent way to identify aggregate roots across the application.
    /// 3. Repository Pattern: Ensures repositories interact only with aggregate roots, not child entities.
    /// 4. Encapsulation: Emphasizes the aggregate root's role in maintaining aggregate integrity.
    /// 5. Domain Logic: Identifies entities that coordinate domain logic within the aggregate.
    /// </summary>
    public interface IAggregateRoot
    {
    }
}