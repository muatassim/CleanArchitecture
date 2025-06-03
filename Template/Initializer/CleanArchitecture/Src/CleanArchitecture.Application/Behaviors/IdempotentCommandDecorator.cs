using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Core.Abstractions;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces.Idempotent;
using CleanArchitecture.Core.Interfaces.Messaging;
using Microsoft.Extensions.Logging; 
namespace CleanArchitecture.Application.Behaviors
{
    internal static class IdempotentCommandDecorator
    {
        internal sealed class CommandHandler<TCommand, TResponse>(
            IIdempotencyRepository idempotencyRepository,
            ICommandHandler<TCommand, TResponse> innerHandler,
            ILogger<CommandHandler<TCommand, TResponse>> logger) : ICommandHandler<TCommand, TResponse>
            where TCommand : ICommand<TResponse> // Added constraint to fix CS0314
        {
            private readonly IIdempotencyRepository idempotencyRepository = idempotencyRepository;
            private readonly ICommandHandler<TCommand, TResponse> innerHandler = innerHandler;
            private readonly ILogger<CommandHandler<TCommand, TResponse>> logger = logger;

            public async Task<Result<TResponse>> Handle(TCommand request, CancellationToken cancellationToken)
            {
                var requestType = typeof(TCommand);
                if (request is not IdempotentCommand<TResponse> idempotentRequest)
                {
                    // Not idempotent, just delegate
                    return await innerHandler.Handle(request, cancellationToken);
                }

               
                logger.LogInformation("Handling Idempotent Command {Command} with RequestId {RequestId}", requestType.Name, idempotentRequest.RequestId);

                var (exists, storedResponse) = await idempotencyRepository.KeyExistsAsync(idempotentRequest.RequestId);
                if (exists)
                {
                    logger.LogInformation("Idempotency key found in repository");
                    if (storedResponse != null)
                    {
                        logger.LogInformation("Returning stored response for Idempotent Command {Command} with RequestId {RequestId}", requestType.Name, idempotentRequest.RequestId);
                        var deserializedResponse = JsonSerializer.Deserialize<TResponse>(storedResponse);
                        return deserializedResponse == null
                            ? throw new InvalidOperationException("Stored response deserialization resulted in null.")
                            : Result.Success(deserializedResponse);
                    }
                    throw new ValidationException("Idempotency key found in repository. Returning BadRequest.");
                }

                var requestJson = JsonSerializer.Serialize(request);
                await idempotencyRepository.StoreKeyAsync(idempotentRequest.RequestId, requestJson);

                var response = await innerHandler.Handle(request, cancellationToken);
                if (response == null)
                {
                    logger.LogWarning("Response is null for idempotent command {Command} with RequestId {RequestId}", requestType.Name, idempotentRequest.RequestId);
                    throw new InvalidOperationException("Response is null");
                }
                return response;
            }
        }
    }
}
