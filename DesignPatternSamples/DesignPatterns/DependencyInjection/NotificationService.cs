namespace DesignPatterns.DependencyInjection
{
    public class NotificationService
    {
        private readonly INotificationSender _sender;

        public NotificationService(INotificationSender sender)
        {
            _sender = sender;
        }

        public void Notify(string message)
        {
            _sender.Send(message);
        }
    }
}
