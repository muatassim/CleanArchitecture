using System.Reflection;
using Microsoft.Extensions.Logging;
using CleanArchitecture.Core.Abstractions;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Infrastructure.MicrosoftSql.Exceptions; 
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
namespace CleanArchitecture.Infrastructure.MicrosoftSql.Repositories
{
    public partial class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options
               , ILogger<ApplicationDbContext> logger) : DbContext(options), IUnitOfWork
    {
        private readonly ILogger<ApplicationDbContext> _logger = logger;

        /// <summary>
        /// Configures the model by applying configurations from the executing assembly.
        /// </summary>
        /// <param name="builder">The model builder.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            IExecutionStrategy strategy = Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await Database.BeginTransactionAsync(cancellationToken);
                try
                {                    
                    var result = await base.SaveChangesAsync(cancellationToken); 
                    await transaction.CommitAsync(cancellationToken); 
                    // Commit the transaction
                    return result;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    _logger.LogError(ex, "Concurrency Exception while saving changes.");
                    await transaction.RollbackAsync(cancellationToken);
                    throw new ConcurrencyException("Concurrency Exception", ex);
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError(ex, "Database Update Exception while saving changes.");
                    await transaction.RollbackAsync(cancellationToken);
                    var message = ex.InnerException?.Message ?? ex.Message;
                    throw new DomainException(new Error("DbError", message));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unexpected Exception while saving changes.");
                    await transaction.RollbackAsync(cancellationToken);
                    var message = ex.InnerException?.Message ?? ex.Message;
                    throw new DomainException(new Error("DbError", message));
                }
            }); 
        }
    }
}
