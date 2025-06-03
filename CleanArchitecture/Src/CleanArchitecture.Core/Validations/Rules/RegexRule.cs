using System.Text.RegularExpressions;
namespace CleanArchitecture.Core.Validations.Rules
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public class RegexRule(string propertyName, string description, string regex) : Rule(propertyName, description)
    {
        private readonly string _regex = regex;
        public override bool ValidateRule(Validator domainObject)
        {
            if (domainObject == null)
            {
                return false;
            }
            var pi = domainObject.GetType().GetProperty(PropertyName);
            if (pi != null)
            {
                var value = pi.GetValue(domainObject, null)?.ToString();
                if (value != null)
                {
                    var m = Regex.Match(value, _regex);
                    if (m.Success)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
