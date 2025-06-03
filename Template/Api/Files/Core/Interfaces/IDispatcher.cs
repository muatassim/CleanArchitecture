using CleanArchitecture.Core.Abstractions;
using CleanArchitecture.Core.Interfaces.Messaging;

namespace CleanArchitecture.Core.Interfaces
{
    public interface IDispatcher
    {
        // For commands with a result (ICommand<TResponse> and IdempotentCommand<TResponse>)
        Task<Result<TResponse>> SendAsync<TCommand, TResponse>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : ICommand<TResponse>;

        // For commands without a result (ICommand and IdempotentCommand)
        Task<Result> SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : ICommand;

        // For queries with a result
        Task<Result<TResponse>> QueryAsync<TQuery, TResponse>(TQuery query, CancellationToken cancellationToken = default)
            where TQuery : IQuery<TResponse>;


       
        //  Task<Result> QueryAsync<TQuery>(TQuery query, CancellationToken cancellationToken = default) where TQuery : IQuery;




    }
}
