namespace DesignPatterns.InterfaceSegragationPrinciple
{
    public class Manager : IWorkable, IManageable
    {
        public void Work() => Console.WriteLine("Manager is working.");
        public void Manage() => Console.WriteLine("Manager is managing.");
    }
}
