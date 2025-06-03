using System.Security.Cryptography;
using System.Text;
namespace CleanArchitecture.Core.Shared
{
    /// <summary>
    /// Provides utility methods for generating cryptographic hashes.
    /// Commonly used for creating idempotency keys, data integrity checks, or secure identifiers.
    /// </summary>
    public static partial class HashGenerator
    {
        /// <summary>
        /// Generates a SHA-256 hash for the given string data and returns it as a lowercase hexadecimal string.
        /// </summary>
        /// <param name="data">The input string to hash.</param>
        /// <returns>The SHA-256 hash as a hexadecimal string.</returns>
        public static string GenerateHash(string data)
        {
            var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(data));
            var builder = new StringBuilder();
            foreach (var byteValue in hashedBytes)
            {
                builder.Append(byteValue.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}