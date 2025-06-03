using CleanArchitecture.Core.Entities.Events;
using CleanArchitecture.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Notifications.Categories
{
    internal sealed class CategoriesCreatedEventNotificationHandler(ILogger<CategoriesCreatedEventNotificationHandler> logger) : IDomainEventHandler<CategoryCreatedEvent>
    {
        public Task Handle(CategoryCreatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Categories created triggered: {Id}", notification.Categories.Id );
            // Add your handling logic here
            return Task.CompletedTask;
        }
    }
}
