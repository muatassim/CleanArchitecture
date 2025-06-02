# CleanArchitecture.Core

This project implements the **Core layer** of a Clean Architecture solution using .NET 9. The Core layer is the heart of the application, containing business logic, domain entities, and interfaces. It is completely independent of external frameworks and technologies.

## What is the Core Layer?

The Core layer defines the essential business rules and domain logic. It should not depend on any other project in the solution. This ensures that the business logic remains isolated and testable.

**Key responsibilities:**
- Define domain entities and value objects
- Specify business rules and logic
- Declare interfaces for services, repositories, and validation
- Contain domain events and exceptions

## Project Structure

- **Entities/**: Domain models (e.g., User, Order)
- **ValueObjects/**: Immutable types representing concepts (e.g., Email, Money)
- **Interfaces/**: Abstractions for repositories, services, etc.
- **Validations/**: Business rule validators
- **Services/**: Domain services (stateless business logic)
- **Exceptions/**: Custom exception types

## Getting Started

1. **Clone the repository** and open the solution in Visual Studio 2022.
2. The Core project targets `.NET 9` and has no dependencies on infrastructure or UI.
3. Add your domain entities, value objects, and interfaces as needed.
4. Reference the Core project from other layers (e.g., Infrastructure, Application, Web).

## Design Principles

- **Independence**: No dependencies on external libraries except for abstractions (e.g., `Microsoft.Extensions.DependencyInjection.Abstractions`).
- **Testability**: All business logic can be unit tested without infrastructure.
- **Separation of Concerns**: Core contains only domain logic, not data access or UI.

## Example: Defining an Entity

## Examples

### 1. Defining an Entity

Defining a Generic Entity and Applying Business Rules
In Clean Architecture, entities should encapsulate their own identity and business rules. You can use a generic base class for entities with different types of identifiers, and enforce business rules using a simple rule pattern.
1. **Define a Customer entity that inherits from the generic base and enforces a business rule (e.g., name must not be empty):**


```C#
namespace CleanArchitecture.Core.Entities
{
    public class Customer : Shared.Entity<int>
    {
        public string Name { get; private set; }

        public Customer(int id, string name) : base(id)
        {
            CheckRule(new CustomerNameCannotBeEmpty(name));
            Name = name;
        }

        private void CheckRule(IRule rule)
        {
            if (rule.IsBroken())
                throw new Exceptions.DomainException(rule.Message);
        }
    }
}
```
2. **Defining a Business Rule** 

```C#
namespace CleanArchitecture.Core.Validations
{
    public interface IRule
    {
        bool IsBroken();
        string Message { get; }
    }

    public class CustomerNameCannotBeEmpty : IRule
    {
        private readonly string _name;
        public CustomerNameCannotBeEmpty(string name) => _name = name;
        public bool IsBroken() => string.IsNullOrWhiteSpace(_name);
        public string Message => "Customer name cannot be empty.";
    }
}
``` 
 
### 2. Creating a Value Object

Value objects are immutable and compared by their values, not identity. For example, an `Email` value object:

```C#
namespace CleanArchitecture.Core.ValueObjects 
{ 
    public record Email(string Value) 
    { 
        public static Email Create(string value) 
        { 
            // Add validation logic here if (string.IsNullOrWhiteSpace(value) || !value.Contains("@")) 
            throw new ArgumentException("Invalid email address.", nameof(value)); return new Email(value); 
         } 
}
```

### 3. Declaring an Interface

Interfaces define contracts for services or repositories. For example, a user repository interface:

```C#
namespace CleanArchitecture.Core.Interfaces 
{ 
    public interface IUserRepository 
    { 
        User? GetById(Guid id); 
        void Add(User user); 
    } 
}
```

### 4. Implementing a Domain Service

Domain services contain business logic that doesn’t naturally fit within an entity or value object. For example, a user registration service:

```C#
namespace CleanArchitecture.Core.Services 
{ 
    public class UserRegistrationService 
    { 
        private readonly IUserRepository _userRepository;
        public UserRegistrationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Register(string email, string name)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = email,
                Name = name
            };
            _userRepository.Add(user);
        }
    }
}
```

### 5. Adding a Custom Exception

Custom exceptions communicate domain-specific errors:
```C#
namespace CleanArchitecture.Core.Exceptions 
{ 
    public class DomainException : Exception 
    { 
        public DomainException(string message) : base(message) 
        {
        } 
    } 
}

```
 

These examples illustrate how to structure your Core layer for maximum clarity, testability, and independence.

## More on Core Layer Structure

### Abstractions

The **Abstractions** folder contains interfaces and contracts that define the boundaries of your domain logic. These abstractions decouple your Core layer from implementation details, allowing infrastructure and application layers to provide concrete implementations without affecting business logic.

**Typical contents:**
- Repository interfaces (e.g., `IUserRepository`)
- Service contracts (e.g., `INotificationService`)
- Unit of Work interfaces

**Example:**

```C#
namespace CleanArchitecture.Core.Abstractions 
{ 
    public interface IRepository<T> 
    { 
        T? GetById(Guid id); 
        void Add(T entity); 
        void Remove(T entity); 
        } 
    }
}
```

### Shared

The **Shared** folder is for code that is used across multiple parts of the Core layer, promoting reuse and consistency. This can include:
- Base classes (e.g., `EntityBase`, `ValueObjectBase`)
- Common types (e.g., `Result<T>`, `PaginatedList<T>`)
- Utility classes or extension methods

**Example:**

```C#
namespace CleanArchitecture.Core.Shared 
{ 
    public abstract class EntityBase 
    { 
        public Guid Id { get; protected set; }
    } 
}
```


### Validations

The **Validations** folder contains business rule validators and logic to ensure domain integrity. These are not just data annotations, but classes or methods that enforce complex business rules.

**Typical contents:**
- Domain validators (e.g., `UserValidator`)
- Specification pattern implementations
- Custom validation exceptions

**Example:**

```C#
namespace CleanArchitecture.Core.Validations 
{ 
    public class UserValidator 
    { 
        public static void ValidateEmail(string email) 
        { 
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))   
                throw new ArgumentException("Invalid email address.", nameof(email));
        } 
    } 
}
```

---

**Summary:**  
- **Abstractions**: Define contracts/interfaces for domain boundaries.
- **Shared**: Common base classes and utilities for reuse.
- **Validations**: Business rule enforcement and domain integrity checks.

This structure keeps your Core layer clean, maintainable, and independent of external dependencies.

## References

- [Clean Architecture by Robert C. Martin](https://8thlight.com/blog/uncle-bob/2012/08/13/the-clean-architecture.html)
- [Microsoft Docs: Clean Architecture](https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures#clean-architecture)

---

**Note:** This Core layer is designed to be stable and reusable across different applications and platforms.
