using System.Globalization;
namespace CleanArchitecture.ApiShared.Helpers
{
    /// <summary>
    ///     Provides utility methods for converting string values to other data types.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        ///     Converts a string to PascalCase.
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        /// <returns>The PascalCase version of the string.</returns>
        public static string Pascalize(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(value).Replace(" ", string.Empty);
        }
        /// <summary>
        ///     Converts a string to camelCase.
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        /// <returns>The camelCase version of the string.</returns>
        public static string Camelize(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;
            value = value.Pascalize();
            if (value.Length > 0)
            {
                return char.ToLowerInvariant(value[0]) + value[1..];
            }
            return value;
        }
    }
}
