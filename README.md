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

- **Reusability**: The Core layer can be reused across different applications or services without modification.
- **Simplicity**: Keep the Core layer focused on business logic, avoiding unnecessary complexity.
- **Consistency**: Follow consistent naming conventions and patterns for entities, value objects, and services.
- **Extensibility**: Design entities and services to be easily extendable for future requirements.
- 


## Example: Defining an Entity
**Why Define Both Generic and Non-Generic Entity Base Classes?**
Generic Entity Base Class (Entity<TEntityId>)
-	Purpose:
Allows you to define entities with any type of identifier (e.g., int, Guid, string).
-	Flexibility:Supports domain models where different entities may use different types for their primary keys.
-	Type Safety:Prevents accidental assignment of IDs of the wrong type at compile time.

Example:
```C#
public class Customer : Entity<int> { /* ... */ }
public class Order : Entity<Guid> { /* ... */ }
```
 Non-Generic Entity Base Class (Entity)
-Purpose:Provides a base for scenarios where the ID type is not important, not yet known, or not needed.
-Framework/Reflection Support:Some frameworks, tools, or generic algorithms may require a non-generic base type for all entities (e.g., for reflection, serialization, or registration).
-Common Functionality:Allows you to share logic (like domain event handling, validation, etc.) across all entities, regardless of their ID type.
```C#
public class AuditLog : Entity { /* ... */ }
```
 How They Work Together
-The generic Entity<TEntityId> can inherit from the non-generic Entity, so all entities share a common base type.
-This enables you to write code that works with all entities (using Entity), or with specific ID types (using Entity<TEntityId>).
```C#
public abstract class Entity : Validator, IAggregateRoot, IEntity { /* ... */ }
public abstract class Entity<TEntityId> : Entity { /* ... */ }
```
##Summary Table
| Use Case                        | Use Entity<TEntityId> | Use Entity      | |----------------------------------|:----------------------:|:-----------------:| | Entity with a specific ID type   | ✔️                     |                   | | Generic code for all entities    |                        | ✔️                | | Reflection, serialization, etc.  |                        | ✔️                | | Common logic for all entities    | ✔️ (via inheritance)    | ✔️                |


## Examples

### 1. Defining an Entity

Defining a Generic Entity and Applying Business Rules
In Clean Architecture, entities should encapsulate their own identity and business rules. You can use a generic base class for entities with different types of identifiers, and enforce business rules using a simple rule pattern.
1. **Define a Person entity that inherits from the generic base and enforces a business rule (e.g., name must not be empty):**


```C#
namespace CleanArchitecture.Core.Entities
{
    public class Person(int id) : Entity<int>(id)
    {
        public int PersonId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Email { get; set; } = string.Empty;

        public override List<Rule> CreateRules()
        {
            var rules = new List<Rule>
            {
                new CustomRule(nameof(Name), $"{nameof(Name)} validation failed.", ()=> PersonValidator.NameIsNotEmpty(Name)),
                new RegexRule(nameof(Email), $"{nameof(Email)} must be valid", @"^[^@\s]+@[^@\s]+\.[^@\s]+$"),
                new CustomRule(nameof(Age), $"{nameof(Age)} validation failed.", ()=> PersonValidator.IsAgeValid(Age)),

            };
            return rules;
        }
    }
}
```
2. **Defining a Validtor ** 

```C#
namespace CleanArchitecture.Core.Validations
{
   public static class PersonValidator
   {
        public static bool NameIsNotEmpty(string name)
        {
            return !string.IsNullOrWhiteSpace(name);
        }
        public static bool IsAgeValid(int age)
        {
            return age is >= 0 and <= 120; // Example validation for age
        }
   }
}
``` 
3. **Defining a Validation Service Not Required **

**Create a validation service to encapsulate validation logic:**
```C#
    public class PersonValidationService : ValidationService<Person, int>
    {
        public override (bool IsValid, List<ValidationError> Errors) IsValid(Person? entity)
        {
            return entity == null ? (false, [new ValidationError("Entity", "Entity is null")]) : base.IsValid(entity);
        }
    }
```
4. ** Validating with and Without Validation Service **

```C#
 [TestClass]
    public class PersonEntityTest
    {
        [DataTestMethod]
        [DataRow(1, "John Doe", "a@gmail.com")]
        public void ValidatePersonRulesTest(int id, string name, string email)
        {
            Person person = new Person(id)
            {
                Name = name,
                Age = 35,
                Email = email
            };
            var rules = person.CreateRules();
            var isValid = rules.All(rule => rule.ValidateRule(person));
            Assert.IsTrue(isValid, "Person entity should be valid based on the provided rules.");
        }

        [DataTestMethod]
        [DataRow(1, "John Doe", "a@gmail.com")]
        public void ValidatePersonRulesServiceTest(int id, string name, string email)
        {
            Person person = new Person(id)
            {
                Name = name,
                Age = 35,
                Email = email
            };
            var personValidationService = new PersonValidationService();
            (bool IsValid, List<ValidationError> Errors)  = personValidationService.IsValid(person);
            Assert.IsTrue(IsValid, "Person entity should be valid based on the provided rules.");
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
