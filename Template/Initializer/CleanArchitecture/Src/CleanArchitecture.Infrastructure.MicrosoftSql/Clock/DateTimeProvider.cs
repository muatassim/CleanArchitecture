using CleanArchitecture.Core.Interfaces; 
namespace CleanArchitecture.Infrastructure.MicrosoftSql.Clock
{
    public sealed class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
