using Microsoft.Extensions.Logging;
using CleanArchitecture.Core.Abstractions;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Interfaces.Messaging;
namespace CleanArchitecture.Application.Behaviors
{
    internal static class ValidationDecorator
    {
        //
        internal sealed class CommandHandler<TCommand, TResponse>(  IValidationHandler<TCommand> validationHandler,  ICommandHandler<TCommand, TResponse> innerHandler,
            ILogger<CommandHandler<TCommand, TResponse>> logger)    : ICommandHandler<TCommand, TResponse>
            where TCommand : ICommand<TResponse>
        {
            public async Task<Result<TResponse>> Handle(TCommand request, CancellationToken cancellationToken)
            {
                var commandName = typeof(TCommand).Name;
                var result = await validationHandler.Validate(request);
                if (!result.IsSuccessful)
                {
                    logger.LogWarning("Validation failed for {Request}. Error: {Error}", commandName, result.Error);
                    throw new ValidationException(result.Errors);
                }
                logger.LogInformation("Validation successful for {Request}.", commandName);
                return await innerHandler.Handle(request, cancellationToken);
            }
        }
    }
}
