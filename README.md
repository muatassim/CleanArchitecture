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

## References

- [Clean Architecture by Robert C. Martin](https://8thlight.com/blog/uncle-bob/2012/08/13/the-clean-architecture.html)
- [Microsoft Docs: Clean Architecture](https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures#clean-architecture)

---

**Note:** This Core layer is designed to be stable and reusable across different applications and platforms.
