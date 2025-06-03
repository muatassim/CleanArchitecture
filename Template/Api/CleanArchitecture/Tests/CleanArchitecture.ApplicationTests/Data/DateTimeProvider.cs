using CleanArchitecture.Core.Interfaces;
namespace CleanArchitecture.ApplicationTests.Data
{
    public sealed class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
