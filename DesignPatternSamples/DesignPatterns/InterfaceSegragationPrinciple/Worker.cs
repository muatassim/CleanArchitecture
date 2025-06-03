namespace DesignPatterns.InterfaceSegragationPrinciple
{
    public class Worker : IWorkable, IEatable
    {
        public void Work() => Console.WriteLine("Worker is working.");
        public void Eat() => Console.WriteLine("Worker is eating.");
    }
}
