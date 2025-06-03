namespace CleanArchitecture.Infrastructure.MicrosoftSql.Constants
{
    public static class Database
    {
        public const string Name = "CleanArchitecture";
        public static class Outbox
        {
            public const string Name = "outbox";
            public static class Tables
            {
                public static class OutBoxMessage
                {
                    public const string Name = "OutBoxMessage";
                    public static class Columns
                    {
                        public const string Id = "Id";
                        public const string Content = "Content";
                        public const string EventType = "Type";
                        public const string OccurredOnUtc = "OccurredOnUtc";
                        public const string ProcessedOnUtc = "ProcessedOnUtc";
                        public const string Error = "Error";
                        public const string IdempotencyKey = "IdempotencyKey";
                    }
                }
            }
        }

        public static class Dbo
        {
            public const string Name = "dbo";
            public static class Tables
            {
                public static class Categories
                {
                    public const string Name = "Categories";
                    public static class Columns
                    {
                        public const string Id = "CategoryID";
                        public const string CategoryName = "CategoryName";
                        public const string Description = "Description";
                        public const string Picture = "Picture";
                    }
                }
            }
        }
    }
}
