namespace DesignPatterns.FactoryPattern
{
    public class SmsNotification : INotification
    {
        public void Send(string message)
        {
            Console.WriteLine($"SMS: {message}");
        }
    }
}
