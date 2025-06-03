using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
namespace CleanArchitecture.Infrastructure.MicrosoftSql.Interceptors
{
    public class LoggingSavingChangesInterceptor(ILogger<LoggingSavingChangesInterceptor> logger) : SaveChangesInterceptor
    {
        private readonly ILogger<LoggingSavingChangesInterceptor> _logger = logger;

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
          DbContextEventData eventData,
          InterceptionResult<int> result,
          CancellationToken cancellationToken = new CancellationToken())
        {
            if (eventData.Context?.ChangeTracker?.DebugView?.LongView != null)
            {
                _logger.LogInformation("ChangeTracker DebugView: {DebugView}", eventData.Context.ChangeTracker.DebugView.LongView);
            }
            else
            {
                _logger.LogInformation("ChangeTracker DebugView is null or unavailable.");
            }

            return new ValueTask<InterceptionResult<int>>(result);
        }
    }
}
