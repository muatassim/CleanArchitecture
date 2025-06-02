namespace CleanArchitecture.Core.Validations.Rules
{
    public class RequiredFieldRule : Rule
    {
        private readonly string _value;
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="propertyName">The name of the property the rule is based on.</param>
        /// <param name="brokenDescription">A description of the rule that will be shown if the rule is broken.</param>
        /// <param name="value">The value to check.</param>
        public RequiredFieldRule(string propertyName, string brokenDescription, string value)
            : base(propertyName, brokenDescription) => _value = value;
        public RequiredFieldRule(string propertyName, string value)
          : base(propertyName, $"{propertyName} is required") => _value = value;
        /// <summary>
        /// Validates that the rule has not been broken.
        /// </summary>
        /// <param name="domainObject">The domain object being validated.</param>
        /// <returns>True if the rule has not been broken, or false if it has.</returns>
        public override bool ValidateRule(Validator domainObject)
        {
            return !string.IsNullOrEmpty(_value);
        }
    }
}
