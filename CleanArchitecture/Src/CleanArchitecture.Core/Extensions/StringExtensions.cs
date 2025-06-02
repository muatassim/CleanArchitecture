using System.Globalization;

namespace CleanArchitecture.Core.Extensions
{
    /// <summary>
    /// Provides utility methods for converting string values to other data types and casing styles.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Converts a string to PascalCase.
        /// Example: "hello world" => "HelloWorld"
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        /// <returns>The PascalCase version of the string.</returns>
        public static string PascalCase(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(value).Replace(" ", string.Empty);
        }

        /// <summary>
        /// Converts a string to camelCase.
        /// Example: "hello world" => "helloWorld"
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        /// <returns>The camelCase version of the string.</returns>
        public static string CamelCase(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;
            value = value.PascalCase();
            if (value.Length > 0)
            {
                return char.ToLowerInvariant(value[0]) + value[1..];
            }
            return value;
        }
    }
}