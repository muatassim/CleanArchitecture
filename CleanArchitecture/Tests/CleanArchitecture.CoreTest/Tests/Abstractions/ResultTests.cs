using CleanArchitecture.Core.Abstractions;

namespace CleanArchitecture.CoreTest.Tests.Abstractions
{
    [TestClass]
    public class ResultTests
    {
        [TestMethod]
        public void Success_CreatesSuccessfulResult()
        {
            var result = Result.Success();
            Assert.IsTrue(result.IsSuccess);
            Assert.IsFalse(result.IsFailure);
            Assert.AreEqual(0, result.Errors.Count);
        }

        [TestMethod]
        public void Failure_CreatesFailedResult_WithSingleError()
        {
            var error = new Error("TestError", "Something went wrong");
            var result = Result.Failure(error);
            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual(1, result.Errors.Count);
            Assert.AreEqual(error, result.Errors[0]);
        }

        [TestMethod]
        public void Failure_CreatesFailedResult_WithMultipleErrors()
        {
            var errors = new List<Error>
            {
                new("Error1", "First error"),
                new("Error2", "Second error")
            };
            var result = Result.Failure(errors);
            Assert.IsFalse(result.IsSuccess);
            Assert.IsTrue(result.IsFailure);
            CollectionAssert.AreEqual(errors, result.Errors);
        }

        [TestMethod]
        public void SuccessT_CreatesSuccessfulResultWithValue()
        {
            var value = 42;
            var result = Result.Success(value);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(value, result.Value);
            Assert.AreEqual(0, result.Errors.Count);
        }

        [TestMethod]
        public void FailureT_CreatesFailedResultWithError()
        {
            var error = new Error("TestError", "Something went wrong");
            var result = Result.Failure<int>(error);
            Assert.IsFalse(result.IsSuccess);
            Assert.ThrowsExactly<InvalidOperationException>(() => { var v = result.Value; });
            Assert.AreEqual(1, result.Errors.Count);
            Assert.AreEqual(error, result.Errors[0]);
        }

        [TestMethod]
        public void FailureT_CreatesFailedResultWithMultipleErrors()
        {
            var errors = new List<Error>
            {
                new("Error1", "First error"),
                new("Error2", "Second error")
            };
            var result = Result.Failure<int>(errors);
            Assert.IsFalse(result.IsSuccess);
            Assert.ThrowsExactly<InvalidOperationException>(() => { var v = result.Value; });
            CollectionAssert.AreEqual(errors, result.Errors);
        }

        [TestMethod]
        public void Create_ReturnsSuccess_WhenValueIsNotNull()
        {
            var value = "test";
            var result = Result.Create(value);
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual(value, result.Value);
        }

        [TestMethod]
        public void Create_ReturnsFailure_WhenValueIsNull()
        {
            var result = Result.Create<string>(null);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual(1, result.Errors.Count);
        }

        [TestMethod]
        public void ImplicitConversion_WorksForSuccess()
        {
            Result<string> result = "hello";
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("hello", result.Value);
        }
 

        [TestMethod]
        public void Constructor_ThrowsForInvalidState_FailureWithNoErrors()
        {
            Assert.ThrowsExactly<InvalidOperationException>(() => Result.Create(false, []));
        }
    }
}