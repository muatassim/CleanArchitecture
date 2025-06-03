namespace DesignPatterns.FactoryPattern
{
    public static class NotificationFactory
    {
        public static INotification Create(string type)
        {
            return type switch
            {
                "email" => new EmailNotification(),
                "sms" => new SmsNotification(),
                _ => throw new ArgumentException("Invalid notification type", nameof(type))
            };
        }
    }
}
