using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using System.Text.Json.Serialization;
namespace CleanArchitecture.Core.Entities.Events
{
    [method: JsonConstructor]
    public class CategoryUpdatedEvent(Categories employees) : IDomainEvent
    {
        // public EmployeesUpdatedEvent() { }
        public Categories Categories { get; set; } = employees;
    }
}
