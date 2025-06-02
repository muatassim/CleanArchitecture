namespace CleanArchitecture.Core.Validations.Rules
{
    /// <summary>
    /// Validation rule that checks if the length of a string field does not exceed a specified maximum.
    /// Inherits from <see cref="Rule"/> and is typically used in domain or input validation scenarios.
    /// </summary>
    public class MaximumFieldLengthRule : Rule
    {
        private readonly string _value;
        private readonly int _maximumAllowedLength;

        /// <summary>
        /// Creates a rule with a custom broken description.
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="brokenDescription">Custom description for when the rule is broken.</param>
        /// <param name="value">The value to validate (must be a string).</param>
        /// <param name="maximumAllowedLength">Maximum allowed length (inclusive).</param>
        public MaximumFieldLengthRule(string propertyName, string brokenDescription, object value, int maximumAllowedLength)
            : base(propertyName, brokenDescription)
        {
            if (value is string stringValue)
            {
                _value = stringValue;
            }
            else
            {
                throw new ArgumentException("Value must be a string", nameof(value));
            }
            _maximumAllowedLength = maximumAllowedLength;
        }

        /// <summary>
        /// Creates a rule with a default broken description.
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="value">The value to validate (must be a string).</param>
        /// <param name="maximumAllowedLength">Maximum allowed length (inclusive).</param>
        public MaximumFieldLengthRule(string propertyName, object value, int maximumAllowedLength)
            : base(propertyName, $"Maximum Length Exceeded for {propertyName}")
        {
            if (value is string stringValue)
            {
                _value = stringValue;
            }
            else
            {
                throw new ArgumentException("Value must be a string", nameof(value));
            }
            _maximumAllowedLength = maximumAllowedLength;
        }

        /// <summary>
        /// Validates that the string value's length does not exceed the allowed maximum.
        /// </summary>
        /// <param name="domainObject">The domain object being validated (not used in this rule).</param>
        /// <returns>True if valid; false otherwise.</returns>
        public override bool ValidateRule(Validator domainObject)
        {
            if (_value == null) return true;
            var isValid = true;
            try
            {
                if (_value.Length > _maximumAllowedLength)
                {
                    isValid = false;
                }
            }
            catch
            {
                isValid = false;
            }
            return isValid;
        }
    }
}