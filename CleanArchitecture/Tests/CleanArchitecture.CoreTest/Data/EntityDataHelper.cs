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
    }
}
