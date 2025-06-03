namespace CleanArchitecture.Core.Entities;

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


