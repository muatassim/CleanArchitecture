using CleanArchitecture.Core.Entities.Events;
using CleanArchitecture.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Notifications.Categories
{
    internal sealed class CategoriesUpdatedEventNotificationHandler(ILogger<CategoriesUpdatedEventNotificationHandler> logger) : IDomainEventHandler<CategoryUpdatedEvent>
    {
        public Task Handle(CategoryUpdatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Categories Updated triggered: {Id}", notification.Categories.Id );
            // Add your handling logic here
            return Task.CompletedTask;
        }
    }
}
