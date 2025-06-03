namespace CleanArchitecture.Core.Validations.Rules
{
    /// <summary>
    /// A class to define a simple rule, using a delegate for validation.
    /// </summary>
    /// <remarks>
    /// Constructor.
    /// </remarks>
    /// <param name="propertyName">The name of the property this rule validates for. This may be blank.</param>
    /// <param name="brokenDescription">A description message to show if the rule has been broken.</param>
    /// <param name="ruleDelegate">A delegate that takes no parameters and returns a boolean value, used to validate the rule.</param>
    public class CustomRule(string propertyName, string brokenDescription, CustomRuleDelegate ruleDelegate) : Rule(propertyName, brokenDescription)
    {
        /// <summary>
        /// Gets or sets the delegate used to validate this rule.
        /// </summary>
        protected CustomRuleDelegate RuleDelegate { get; set; } = ruleDelegate;
        /// <summary>
        /// Validates that the rule has not been broken.
        /// </summary>
        /// <param name="domainObject">The domain object being validated.</param>
        /// <returns>True if the rule has not been broken, or false if it has.</returns>
        public override bool ValidateRule(Validator domainObject)
        {
            return RuleDelegate();
        }
    }
}
