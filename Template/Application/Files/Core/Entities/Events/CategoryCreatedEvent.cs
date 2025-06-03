using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using System.Text.Json.Serialization;
namespace CleanArchitecture.Core.Entities.Events;

[method: JsonConstructor]
public class CategoryCreatedEvent(Categories categories) : IDomainEvent
{
    //public EmployeesCreatedEvent() { }
    public Categories Categories { get; set; } = categories;
}
