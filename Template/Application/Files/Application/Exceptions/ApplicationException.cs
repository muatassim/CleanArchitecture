using CleanArchitecture.Core.Abstractions;
using System.Text;  
namespace CleanArchitecture.Application.Exceptions
{
    /// <summary>
    /// application Exception
    /// </summary>
    public partial class ApplicationException : Exception
    {
        public readonly IReadOnlyCollection<Error> Errors;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="errors"></param>
        public ApplicationException(List<Error> errors) : base("Validation Errors") => Errors = errors;
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="error"></param>
        public ApplicationException(Error error) : base(error.Message) => Errors = [error];
        public ApplicationException(string error) : base(error) => Errors = [new("", error)];
        public ApplicationException(string name, string error) : base(error, new ArgumentException(error)) => Errors = [new Error(name, error)];
        public ApplicationException(string message, Exception exception) : base(message, exception) => Errors = [];
        public override string ToString()
        {
            if (Errors != null)
            {
                StringBuilder sb = new();
                foreach (var error in Errors)
                {
                    sb.AppendLine($"name: {error.Name}= message:{error.Message}");
                }
                return sb.ToString();
            }
            return $"{base.ToString()}";
        }
    }
}
