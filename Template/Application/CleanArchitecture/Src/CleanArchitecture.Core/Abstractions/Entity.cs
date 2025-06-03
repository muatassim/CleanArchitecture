using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Validations;

namespace CleanArchitecture.Core.Abstractions
{
    /// <summary>
    /// Represents the base entity class that other entity classes can inherit from.
    /// Provides common properties and methods for handling notifications and entity identity.
    /// </summary>
    public abstract class Entity<TEntityId>(TEntityId id) : Validator, IAggregateRoot, IEntity
    {
        /// <summary>
        /// Gets the unique identifier for the entity.
        /// </summary>
        public TEntityId Id { get; init; } = id;
        /// <summary>
        /// Initializes a new instance of the Entity class.
        /// </summary>
        int? _requestedHashCode;
        /// <summary>
        /// Initializes a new instance of the Entity class.
        /// </summary>
        private readonly List<IDomainEvent> _domainEvents = [];
        public IReadOnlyList<IDomainEvent> GetDomainEvents()
        {
            return _domainEvents?.ToList() ?? [];
        }
        /// <summary>
        /// Adds a domainEvent to the entity.
        /// </summary>
        /// <param name="domainEvent">The domainEvent to add.</param>
        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
        /// <summary>
        /// Removes a domainEvent from the entity.
        /// </summary>
        /// <param name="domainEvent">The domainEvent to remove.</param>
        public void RemoveDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents?.Remove(domainEvent);
        }
        /// <summary>
        /// Clears all notifications from the entity.
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
        /// <summary>
        /// Determines whether the entity has any notifications.
        /// </summary>
        /// <returns>True if the entity has notifications; otherwise, false.</returns>
        public virtual bool HasDomainEvents()
        {
            return _domainEvents?.Count > 0;
        }
        /// <summary>
        /// Determines whether the specified object is equal to the current entity.
        /// </summary>
        /// <param name="obj">The object to compare with the current entity.</param>
        /// <returns>True if the specified object is equal to the current entity; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null || obj is not Entity<TEntityId>) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;
            var baseEntity = (Entity<TEntityId>)obj;
            return baseEntity.Id != null && baseEntity.Id.Equals(Id);
        }
        /// <summary>
        /// Determines whether the entity is transient (i.e., not persisted to the database).
        /// </summary>
        /// <returns>True if the entity is transient; otherwise, false.</returns>
        public bool IsTransient()
        {
            return Id == null || Id.Equals(default(TEntityId));
        }
        /// <summary>
        /// Serves as the default hash function.
        /// The use of ^ 31 in the GetHashCode method is a common technique to combine hash codes in a way that
        /// reduces the likelihood of hash collisions. The ^ operator is the bitwise XOR operator, 
        /// and 31 is a prime number. Using a prime number helps to distribute the hash codes more uniformly.
        /// Here is a detailed explanation of why ^ 31 is used:
        /// 1.	Bitwise XOR Operator(^) : The XOR operator combines the bits of two numbers.
        ///     If the bits are the same, the result is 0; if the bits are different, the result is 1. 
        ///     This operation helps to mix the bits of the hash code, making it less likely for different objects
        ///     to produce the same hash code.
        /// 2.	Prime Number (31): Prime numbers are often used in hash code calculations because 
        ///     they help to distribute the hash codes more uniformly.This reduces the likelihood 
        ///     of hash collisions, where different objects produce the same hash code.
        /// 3.	Combining Hash Codes: By XORing the hash code of Id with 31, 
        ///     we are effectively combining the hash code with a prime number, 
        ///     which helps to ensure that the resulting hash code is more unique and 
        ///     less likely to collide with other hash codes.
        /// </summary>
        /// <returns>A hash code for the current entity.</returns>
        public override int GetHashCode()
        {
            if (IsTransient())
            {
                return base.GetHashCode();
            }
            else
            {
                if (!_requestedHashCode.HasValue && Id != null)
                {
                    _requestedHashCode = Id.GetHashCode() ^ 31;
                }
                return _requestedHashCode ?? base.GetHashCode();
            }
        }
    }
    public abstract class Entity : Validator, IAggregateRoot, IEntity
    {
        /// <summary>
        /// Initializes a new instance of the Entity class.
        /// </summary>
        private readonly List<IDomainEvent> _domainEvents = [];
        public IReadOnlyList<IDomainEvent> GetDomainEvents()
        {
            return _domainEvents?.ToList() ?? [];
        }
        /// <summary>
        /// Adds a domainEvent to the entity.
        /// </summary>
        /// <param name="domainEvent">The domainEvent to add.</param>
        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
        /// <summary>
        /// Removes a domainEvent from the entity.
        /// </summary>
        /// <param name="domainEvent">The domainEvent to remove.</param>
        public void RemoveDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents?.Remove(domainEvent);
        }
        /// <summary>
        /// Clears all notifications from the entity.
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
        /// <summary>
        /// Determines whether the entity has any notifications.
        /// </summary>
        /// <returns>True if the entity has notifications; otherwise, false.</returns>
        public virtual bool HasDomainEvents()
        {
            return _domainEvents?.Count > 0;
        }
        /// <summary>
        /// Determines whether the specified object is equal to the current entity.
        /// </summary>
        /// <param name="obj">The object to compare with the current entity.</param>
        /// <returns>True if the specified object is equal to the current entity; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
