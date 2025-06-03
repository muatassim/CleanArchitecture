using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
namespace CleanArchitecture.Core.Validations
{
    /// <summary>
    /// Provides a base class for implementing validation logic using rules.
    /// Supports property change notification and recursive validation of object graphs.
    /// </summary>
    public abstract class Validator : INotifyPropertyChanged
    {
        private List<Rule> _rules;
        protected Validator() => _rules = [];

        #region Property Changed Event
        private event PropertyChangedEventHandler? PropertyChangedEvent;
        private readonly List<PropertyChangedEventHandler> _propertyChangedSubscribers = [];
        public event PropertyChangedEventHandler? PropertyChanged
        {
            add
            {
                if (value != null && !_propertyChangedSubscribers.Contains(value))
                {
                    PropertyChangedEvent += value;
                    _propertyChangedSubscribers.Add(value);
                }
            }
            remove
            {
                if (value != null && _propertyChangedSubscribers.Remove(value))
                {
                    PropertyChangedEvent -= value;
                }
            }
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEvent?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var propertyName = ExtractPropertyName(propertyExpression);
            OnPropertyChanged(propertyName);
        }
        public static string ExtractPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            ArgumentNullException.ThrowIfNull(propertyExpression);
            if (propertyExpression.Body is not MemberExpression memberExpression) throw new ArgumentException("Invalid expression", nameof(propertyExpression));
            if (memberExpression.Member is not PropertyInfo property) throw new ArgumentException("Member is not a property", nameof(propertyExpression));
            if (property.GetGetMethod(true)?.IsStatic == true) throw new ArgumentException("Static property", nameof(propertyExpression));
            return memberExpression.Member.Name;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Recursively walks the object graph, applying validation or collection logic to each Validator and IList found.
        /// </summary>
        protected void WalkObjectGraph(Func<Validator, bool> snippetForObject, Action<IList> snippetForCollection, params string[] exemptProperties)
        {
            var visited = new HashSet<Validator>();
            var exemptions = new HashSet<string>(exemptProperties);
            void Walk(Validator o)
            {
                if (o == null || !visited.Add(o)) return;
                if (!snippetForObject.Invoke(o))
                {
                    var properties = o.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    foreach (var property in properties)
                    {
                        if (exemptions.Contains(property.Name)) continue;
                        var propertyValue = property.GetValue(o);
                        if (propertyValue is Validator validator)
                        {
                            Walk(validator);
                        }
                        else if (propertyValue is IList list)
                        {
                            snippetForCollection.Invoke(list);
                            foreach (var item in list.OfType<Validator>())
                            {
                                Walk(item);
                            }
                        }
                    }
                }
            }
            Walk(this);
        }

        /// <summary>
        /// Returns whether the object is valid and a list of validation errors.
        /// </summary>
        public virtual (bool IsValid, List<ValidationError> Errors) IsValid()
        {
            var errors = GetErrors();
            if (errors != null && errors.Count == 0)
                return (true, errors);
            return (false, errors ?? []);
        }

        /// <summary>
        /// Returns a list of validation errors for the object.
        /// </summary>
        public virtual List<ValidationError> GetErrors()
        {
            var result = GetBrokenRules().Select(r => new ValidationError(r.PropertyName, r.Description)).ToList();
            return result;
        }

        /// <summary>
        /// Returns a read-only collection of broken rules for the object.
        /// </summary>
        ReadOnlyCollection<Rule> GetBrokenRules()
        {
            if (_rules.Count == 0) _rules = CreateRules();
            var broken = new List<Rule>();
            foreach (var rule in _rules)
            {
                try
                {
                    if (!rule.ValidateRule(this))
                    {
                        broken.Add(rule);
                        Debug.WriteLine($"{DateTime.Now}: Validating rule '{rule}' on object '{this}'. Result = Broken");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"{DateTime.Now}: Exception validating rule '{rule}' on object '{this}': {ex.Message}");
                    broken.Add(rule);
                }
            }
            return broken.AsReadOnly();
        }

        /// <summary>
        /// Override this method to define the validation rules for the object.
        /// </summary>
        public virtual List<Rule> CreateRules()
        {
            return [];
        }
        #endregion

        /// <summary>
        /// Utility to trim and clean strings.
        /// </summary>
        protected static string CleanString(string s)
        {
            return (s ?? string.Empty).Trim();
        }
    }
}