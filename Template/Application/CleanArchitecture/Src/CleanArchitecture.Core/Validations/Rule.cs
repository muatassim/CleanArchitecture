namespace CleanArchitecture.Core.Validations
{
    /// <summary>
    /// An abstract class that contains information about a rule as well as a method to validate it.
    /// </summary>
    /// <remarks>
    /// This class is primarily designed to be used on a domain object to validate a business rule. In most cases, you will want to use the 
    /// concrete class CustomRule, which just needs you to supply a delegate used for validation. For custom, complex business rules, you can 
    /// extend this class and provide your own method to validate the rule.
    /// </remarks>
    public abstract class Rule(string propertyName, string brokenDescription)
    {
        private string _propertyName = propertyName?.Trim() ?? string.Empty;
        public string Description { get; set; } = brokenDescription;
        public string PropertyName
        {
            get => _propertyName;
            protected set => _propertyName = value?.Trim() ?? string.Empty;
        }
        public abstract bool ValidateRule(Validator domainObject);
        public override string ToString() => Description;
    }
}
