using CleanArchitecture.Core.Shared;

namespace CleanArchitecture.CoreTest.Tests.Shared
{
    [TestClass]
    public class HashGeneratorTests
    {
        [TestMethod]
        public void GenerateHash_ReturnsConsistentHash_ForSameInput()
        {
            var input = "test string";
            var hash1 = HashGenerator.GenerateHash(input);
            var hash2 = HashGenerator.GenerateHash(input);

            Assert.AreEqual(hash1, hash2, "Hashes should be consistent for the same input.");
        }

        [TestMethod]
        public void GenerateHash_ReturnsDifferentHash_ForDifferentInput()
        {
            var input1 = "string one";
            var input2 = "string two";
            var hash1 = HashGenerator.GenerateHash(input1);
            var hash2 = HashGenerator.GenerateHash(input2);

            Assert.AreNotEqual(hash1, hash2, "Hashes should differ for different input.");
        }

        [TestMethod]
        public void GenerateHash_ReturnsHash_ForEmptyString()
        {
            var hash = HashGenerator.GenerateHash(string.Empty);

            Assert.IsFalse(string.IsNullOrWhiteSpace(hash), "Hash should not be null or whitespace for empty input.");
            Assert.AreEqual(64, hash.Length, "SHA-256 hash should be 64 hex characters.");
        }

        [TestMethod]
        public void GenerateHash_ReturnsHash_ForUnicodeString()
        {
            var input = "тестовая строка";
            var hash = HashGenerator.GenerateHash(input);

            Assert.IsFalse(string.IsNullOrWhiteSpace(hash), "Hash should not be null or whitespace for unicode input.");
            Assert.AreEqual(64, hash.Length, "SHA-256 hash should be 64 hex characters.");
        }

        [TestMethod]
        public void GenerateHash_ThrowsArgumentNullException_ForNullInput()
        {
            Assert.ThrowsExactly<ArgumentNullException>(() => HashGenerator.GenerateHash(null!));
        }
    }
}