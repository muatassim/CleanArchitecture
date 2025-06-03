namespace CleanArchitecture.Core.Interfaces
{
    /// <summary>
    /// Defines the contract for a domain entity that can hold and manage domain events.
    /// Enables entities to collect domain events during business operations, which can later be dispatched.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets the list of domain events associated with this entity.
        /// </summary>
        IReadOnlyList<IDomainEvent> GetDomainEvents();

        /// <summary>
        /// Clears all domain events from the entity.
        /// </summary>
        void ClearDomainEvents();
    }
}