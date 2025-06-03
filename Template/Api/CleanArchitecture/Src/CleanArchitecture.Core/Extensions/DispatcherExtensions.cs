using CleanArchitecture.Core.Extensions;
using CleanArchitecture.Core.Abstractions;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Interfaces.Messaging;
namespace CleanArchitecture.Core.Extensions;


public static class DispatcherExtensions
{
    public static Task<Result<TResponse>> QueryAsync<TQuery, TResponse>(
        this IDispatcher dispatcher,
        TQuery query,
        CancellationToken cancellationToken = default)
        where TQuery : IQuery<TResponse>
    {
        return dispatcher.QueryAsync<TQuery, TResponse>(query, cancellationToken);
    }

    // This method allows you to call QueryAsync(query) and have the types inferred
    public static Task<Result<TResponse>> QueryAsync<TResponse>(
        this IDispatcher dispatcher,
        IQuery<TResponse> query,
        CancellationToken cancellationToken = default)
    {
        // Use reflection to call the correct generic method
        var method = typeof(IDispatcher).GetMethod(nameof(IDispatcher.QueryAsync)) ?? throw new InvalidOperationException($"Method '{nameof(IDispatcher.QueryAsync)}' not found on type '{typeof(IDispatcher)}'.");
        var genericMethod = method.MakeGenericMethod(query.GetType(), typeof(TResponse));
        return (Task<Result<TResponse>>)genericMethod.Invoke(dispatcher, [query, cancellationToken])!;
    }

   
    public static Task<Result> SendAsync<TCommand>(this IDispatcher dispatcher, TCommand command, CancellationToken cancellationToken = default)
      where TCommand : ICommand
    {
        return dispatcher.SendAsync<TCommand>(command, cancellationToken);
    }
    /// <summary>
    /// Allows calling SendAsync(command) for commands implementing ICommand<TResponse>
    /// and infers the response type automatically.
    /// </summary>
    public static Task<Result<TResponse>> SendAsync<TResponse>(
        this IDispatcher dispatcher,
        ICommand<TResponse> command,
        CancellationToken cancellationToken = default)
    {
        // Use the generic method on IDispatcher
        return dispatcher.SendAsync<ICommand<TResponse>, TResponse>(command, cancellationToken);
    }
 
}
 

