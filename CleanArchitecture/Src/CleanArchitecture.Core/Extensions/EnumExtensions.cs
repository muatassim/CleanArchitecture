using System.ComponentModel;
using System.Reflection;

namespace CleanArchitecture.Core.Extensions
{
    /// <summary>
    /// Provides extension methods for working with enums, especially for handling Description attributes and parsing.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the description of an enum value from its DescriptionAttribute, or the enum name if not present.
        /// </summary>
        public static string GetDescription(this Enum value)
        {
            FieldInfo? field = value.GetType().GetField(value.ToString());
            if (field == null) return value.ToString();
            return Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is not DescriptionAttribute attribute ? value.ToString() : attribute.Description;
        }

        /// <summary>
        /// Gets the enum value of type T from a description string. Throws if not found.
        /// </summary>
        public static T GetValueFromDescription<T>(string description) where T : Enum
        {
            Type type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null)!;
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null)!;
                }
            }
            throw new ApplicationException($"Description Attribute not found{description}");
        }

        /// <summary>
        /// Gets the description of an enum value (generic version).
        /// </summary>
        public static string GetDescription<T>(this T e) where T : Enum
        {
            string description = string.Empty;
            if (e is Enum)
            {
                description = GetEnumDescription(e);
            }
            return description;
        }

        /// <summary>
        /// Parses a string to an enum value of type T using the DescriptionAttribute, or returns the provided default value.
        /// </summary>
        public static T ParseEnum<T>(this string stringValue, T defaultValue)
        {
            Type type = typeof(T);
            if (!type.IsEnum)
            {
                throw new ArgumentException("T must be of Enum type", nameof(stringValue));
            }
            MemberInfo[] fields = type.GetFields();
            foreach (var field in fields)
            {
                DescriptionAttribute[] attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes != null && attributes.Length > 0 && attributes[0].Description == stringValue)
                {
                    return (T)Enum.Parse(typeof(T), field.Name);
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// Helper to get the description of an enum value.
        /// </summary>
        static string GetEnumDescription(Enum value)
        {
            FieldInfo? fi = value.GetType().GetField(value.ToString());
            if (fi == null) return value.ToString();
            if (fi.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            return value.ToString();
        }
    }
}