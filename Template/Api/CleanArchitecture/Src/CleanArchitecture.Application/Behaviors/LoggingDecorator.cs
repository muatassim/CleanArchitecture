using CleanArchitecture.Core.Interfaces.Messaging;
using Microsoft.Extensions.Logging;
using CleanArchitecture.Core.Abstractions;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace CleanArchitecture.Application.Behaviors
{
    internal static class LoggingDecorator
    {
        internal sealed class QueryHandler<TQuery, TResponse>( IQueryHandler<TQuery, TResponse> innerHandler,  ILogger<QueryHandler<TQuery, TResponse>> logger)
            : IQueryHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
        {
            public async Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken)
            {
                var requestName = typeof(TQuery).Name;
                try
                {
                    logger.LogInformation("Executing Command {Command}", requestName);
                    var result = await innerHandler.Handle(query, cancellationToken);
                    if (result.IsSuccess)
                    {
                        logger.LogInformation("Request {RequestName} processed successfully", requestName);
                    }
                    else
                    {
                        logger.LogError("Request {RequestName} processed with error", requestName);
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error handling request for {PropertyName}", requestName);
                    throw;
                }
            }
        }
    }
}
