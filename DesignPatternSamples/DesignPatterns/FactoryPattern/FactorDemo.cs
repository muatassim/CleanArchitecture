namespace DesignPatterns.FactoryPattern
{
    public class FactorDemo
    {

        public static void Execute()
        {
            var notification = NotificationFactory.Create("email");
            notification.Send("Hello from the factory pattern!");
        }
    }
}
