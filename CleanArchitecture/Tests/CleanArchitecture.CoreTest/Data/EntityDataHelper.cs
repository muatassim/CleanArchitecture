using CleanArchitecture.Core.Entities.Outbox;
using CleanArchitecture.Core.Interfaces;
using System.Text.Json;
namespace CleanArchitecture.CoreTest.Data
{
    public static class EntityDataHelper
    {
        private static readonly DateTimeProvider DateTimeProvider;
        static readonly JsonSerializerOptions JsonSerializerOptions;
        static EntityDataHelper()
        {
            DateTimeProvider = new DateTimeProvider();
            JsonSerializerOptions = new JsonSerializerOptions
            {
                IncludeFields = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }
       
        public static OutboxMessage GetOutboxMessage(IDomainEvent domainEvent, string key)
        {
            Guid id = Guid.NewGuid();
            var eventType = domainEvent.GetType().FullName;
            if (eventType != null)
            {
                return new OutboxMessage()
                {
                    Id = id,
                    EventType = domainEvent.GetType().FullName!,
                    OccurredOnUtc = DateTimeProvider.UtcNow,
                    Error = string.Empty,
                    IdempotencyKey = key,
                    Content = JsonSerializer.Serialize((TestEvent)domainEvent, JsonSerializerOptions)
                };
            }
            return null!;
        }
    }
}
