using CleanArchitecture.Core.Interfaces;

namespace CleanArchitecture.CoreTest.Data
{
    public sealed class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
