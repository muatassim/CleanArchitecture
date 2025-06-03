using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
namespace CleanArchitecture.Infrastructure.MicrosoftSql.Interceptors
{
    public class DatabaseLongQueryLoggerInterceptor(ILogger<DatabaseLongQueryLoggerInterceptor> logger) : DbCommandInterceptor
    {
        private readonly ILogger<DatabaseLongQueryLoggerInterceptor> _logger = logger;
        public override ValueTask<DbDataReader> ReaderExecutedAsync(DbCommand command, CommandExecutedEventData eventData, DbDataReader result, CancellationToken cancellationToken = default)
        {
            if (eventData.Duration.TotalMilliseconds > 2000)
            {
                LogLongQuery(command, eventData);
            }
            return base.ReaderExecutedAsync(command, eventData, result, cancellationToken);
        }
        public override DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
        {
            if (eventData.Duration.TotalMilliseconds > 2000)
            {
                LogLongQuery(command, eventData);
            }
            return base.ReaderExecuted(command, eventData, result);
        }
        private void LogLongQuery(DbCommand command, CommandExecutedEventData eventData)
        {
            _logger.LogWarning("Long query: {CommandText}. TotalMilliseconds: {TotalMilliseconds}", command.CommandText, eventData.Duration.TotalMilliseconds);
        }
    }
}
