namespace CleanArchitecture.Core.Validations.Rules
{
    /// <summary>
    /// Validation rule that checks if the length of a string field is between a minimum and maximum value (inclusive).
    /// Inherits from <see cref="Rule"/> and is typically used in domain or input validation scenarios.
    /// </summary>
    public class FieldLengthBetweenRule : Rule
    {
        private readonly string _value;
        private readonly int _minimumAllowedLength;
        private readonly int _maximumAllowedLength;

        /// <summary>
        /// Creates a rule with a custom broken description.
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="brokenDescription">Custom description for when the rule is broken.</param>
        /// <param name="value">The value to validate (must be a string).</param>
        /// <param name="minimumAllowedLength">Minimum allowed length (inclusive).</param>
        /// <param name="maximumAllowedLength">Maximum allowed length (inclusive).</param>
        public FieldLengthBetweenRule(string propertyName, string brokenDescription,
            object value, int minimumAllowedLength, int maximumAllowedLength)
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
            _minimumAllowedLength = minimumAllowedLength;
            _maximumAllowedLength = maximumAllowedLength;
        }

        /// <summary>
        /// Creates a rule with a default broken description.
        /// </summary>
        /// <param name="propertyName">The name of the property being validated.</param>
        /// <param name="value">The value to validate (must be a string).</param>
        /// <param name="minimumAllowedLength">Minimum allowed length (inclusive).</param>
        /// <param name="maximumAllowedLength">Maximum allowed length (inclusive).</param>
        public FieldLengthBetweenRule(string propertyName, object value, int minimumAllowedLength, int maximumAllowedLength)
            : base(propertyName, $"Length of {propertyName} is not between: {minimumAllowedLength} and {maximumAllowedLength}")
        {
            if (value is string stringValue)
            {
                _value = stringValue;
            }
            else
            {
                throw new ArgumentException("Value must be a string", nameof(value));
            }
            _minimumAllowedLength = minimumAllowedLength;
            _maximumAllowedLength = maximumAllowedLength;
        }

        /// <summary>
        /// Validates that the string value's length is within the allowed range.
        /// </summary>
        /// <param name="domainObject">The domain object being validated (not used in this rule).</param>
        /// <returns>True if valid; false otherwise.</returns>
        public override bool ValidateRule(Validator domainObject)
        {
            if (_value == null) return false;
            var isValid = true;
            try
            {
                if (_value.Length < _minimumAllowedLength || _value.Length > _maximumAllowedLength)
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