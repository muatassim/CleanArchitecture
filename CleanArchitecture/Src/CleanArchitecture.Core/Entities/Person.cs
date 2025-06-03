using CleanArchitecture.Core.Abstractions;
using CleanArchitecture.Core.Validations;
using CleanArchitecture.Core.Validations.Rules;

namespace CleanArchitecture.Core.Entities;

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