using CleanArchitecture.Core.Abstractions;
using CleanArchitecture.Core.Extensions;

namespace CleanArchitecture.Core.Shared
{
    /// <summary>
    /// Provides helper methods for working with enums, such as generating lookup lists for UI dropdowns.
    /// </summary>
    public class EnumHelper
    {
        /// <summary>
        /// Generates a list of <see cref="LookUp"/> objects from the values of the specified enum type.
        /// Each item contains the enum value as the code and its description (from DescriptionAttribute or name) as the description.
        /// The value "NotDefined" (case-insensitive) is excluded from the list.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="e">An instance of the enum (used only for type inference).</param>
        /// <returns>A list of <see cref="LookUp"/> objects representing the enum values.</returns>
        public static List<LookUp> GetLookUpList<T>(T e) where T : Enum
        {
            List<LookUp> lookUpList = [];
            if (e is Enum)
            {
                var enumType = typeof(T);
                var enumValArray = Enum.GetValues(enumType);
                foreach (T val in enumValArray)
                {
                    var description = val.GetDescription();
                    if (!val.ToString().Equals("NotDefined", StringComparison.CurrentCultureIgnoreCase))
                    {
                        LookUp lookUp = new()
                        {
                            Code = val.ToString(),
                            Description = description
                        };
                        lookUpList.Add(lookUp);
                    }
                }
            }
            return lookUpList;
        }
    }
}