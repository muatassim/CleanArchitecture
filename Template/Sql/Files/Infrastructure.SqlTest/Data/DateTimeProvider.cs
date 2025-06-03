using System;
using CleanArchitecture.Core.Interfaces;
namespace CleanArchitecture.Infrastructure.MicrosoftSqlTests.Data
{
    public sealed class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
