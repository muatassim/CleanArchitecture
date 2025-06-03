namespace CleanArchitecture.Core.Validations
{
    /// <summary>
    /// A simple type of domain object rule that uses a delegate for validation. 
    /// </summary>
    /// <returns>True if the rule has been followed, or false if it has been broken.</returns>
    /// <remarks>
    /// Usage:
    /// <code>
    ///     this.Rules.Add(new CustomRule("PropertyName", "The customer name must be at least 5 letters long.", delegate { return this.PropertyName &gt; 5; } ));
    /// </code>
    /// </remarks>
    public delegate bool CustomRuleDelegate();
}
