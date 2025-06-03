namespace CleanArchitecture.Core.Interfaces
{
    /// <summary>
    /// Represents the Unit of Work pattern, which coordinates the writing of changes and the resolution of concurrency problems.
    /// Typically used to group multiple operations (such as repository changes) into a single transaction.
    /// 
    /// <para>
    /// <b>Why use IUnitOfWork with multiple database commands?</b><br/>
    /// When you perform two or more database operations (such as inserts, updates, or deletes) that must succeed or fail together,
    /// the Unit of Work ensures that all changes are committed as a single transaction. If any command fails, all changes are rolled back,
    /// maintaining data consistency and integrity. Without a Unit of Work, partial updates could leave your data in an inconsistent state.
    /// </para>
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Persists all changes made in the current unit of work to the data store.
        /// </summary>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        /// <returns>The number of state entries written to the data store.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}