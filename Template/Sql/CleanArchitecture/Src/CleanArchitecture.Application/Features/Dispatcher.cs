using Microsoft.Extensions.DependencyInjection;
using CleanArchitecture.Core.Abstractions;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Interfaces.Messaging;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace CleanArchitecture.Application.Features
{


    public class Dispatcher(IServiceProvider serviceProvider) : IDispatcher
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        // For commands with a result
        public async Task<Result<TResponse>> SendAsync<TCommand, TResponse>(TCommand command, CancellationToken cancellationToken = default)
           where TCommand : ICommand<TResponse>
        {
            //if (command is IdempotentCommand<TResponse>)
            //{
            //    var handlerType = typeof(IdempotentCommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResponse));
            //    dynamic handler = _serviceProvider.GetRequiredService(handlerType);
            //    return await handler.Handle((dynamic)command, cancellationToken);
            //}
            //else 
            if (command is ICommand<TResponse>)
            {
                var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResponse));
                dynamic handler = _serviceProvider.GetRequiredService(handlerType);
                return await handler.Handle((dynamic)command, cancellationToken);
            }
            throw new InvalidOperationException($"No handler found for command type {typeof(TCommand).Name}");
        }

        // For commands without a result
        public async Task<Result> SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
           where TCommand : ICommand
        {
            
            if (command is ICommand)
            {
                var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
                dynamic handler = _serviceProvider.GetRequiredService(handlerType);
                return await handler.Handle((dynamic)command, cancellationToken);
            }

            throw new InvalidOperationException($"No handler found for command type {typeof(TCommand).Name}");
        }

        // For queries with a result
        public async Task<Result<TResponse>> QueryAsync<TQuery, TResponse>(TQuery query, CancellationToken cancellationToken = default)
            where TQuery : IQuery<TResponse>
        {
            var handler = _serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResponse>>();
            return await handler.Handle(query, cancellationToken);
        }
    }

}
