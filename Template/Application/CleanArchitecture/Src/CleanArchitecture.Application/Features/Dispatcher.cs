using CleanArchitecture.Core.Abstractions;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Interfaces.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace CleanArchitecture.Application.Features
{


    public class Dispatcher(IServiceProvider serviceProvider) : IDispatcher
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        private static readonly ConcurrentDictionary<(Type, Type), Type> CommandHandlerTypeCache = new();
        private static readonly ConcurrentDictionary<Type, Type> CommandHandlerNoResultTypeCache = new();
        private static readonly ConcurrentDictionary<(Type, Type), Type> QueryHandlerTypeCache = new();

        // For commands with a result
        public async Task<Result<TResponse>> SendAsync<TCommand, TResponse>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : ICommand<TResponse>
        {
            if (command is ICommand<TResponse>)
            {
                var handlerType = CommandHandlerTypeCache.GetOrAdd((command.GetType(), typeof(TResponse)), t =>
                    typeof(ICommandHandler<,>).MakeGenericType(t.Item1, t.Item2));
                dynamic handler = _serviceProvider.GetRequiredService(handlerType);
                return await handler.Handle((dynamic)command, cancellationToken).ConfigureAwait(false);
            }
            throw new InvalidOperationException($"No handler found for command type {typeof(TCommand).Name}");
        }

        // For commands without a result
        public async Task<Result> SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : ICommand
        {
            if (command is ICommand)
            {
                var handlerType = CommandHandlerNoResultTypeCache.GetOrAdd(command.GetType(), t =>
                    typeof(ICommandHandler<>).MakeGenericType(t));
                dynamic handler = _serviceProvider.GetRequiredService(handlerType);
                return await handler.Handle((dynamic)command, cancellationToken).ConfigureAwait(false);
            }
            throw new InvalidOperationException($"No handler found for command type {typeof(TCommand).Name}");
        }

        // For queries with a result
        public async Task<Result<TResponse>> QueryAsync<TQuery, TResponse>(TQuery query, CancellationToken cancellationToken = default)
            where TQuery : IQuery<TResponse>
        {
            var handlerType = QueryHandlerTypeCache.GetOrAdd((query.GetType(), typeof(TResponse)), t =>
                typeof(IQueryHandler<,>).MakeGenericType(t.Item1, t.Item2));
            dynamic handler = _serviceProvider.GetRequiredService(handlerType);
            return await handler.Handle((dynamic)query, cancellationToken).ConfigureAwait(false);
        }
    }
}

