using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.InterfaceSegragationPrinciple
{
    // The Interface Segregation Principle (ISP) states that no client should be forced to depend on methods it does not use.
    // This means that interfaces should be designed to be small and specific, allowing clients to implement only the methods they need, rather than being forced to implement a large interface with many unused methods.
    // This principle helps to reduce the impact of changes in interfaces, making the system more flexible and easier to maintain.
    // Interface Segregation Principle – No client should be forced to depend on methods it does not use.
    // In this example, we have two interfaces: IWorkable and IEatable. The Worker class implements both interfaces, while the Manager class implements IWorkable and a new interface IManageable.
    // This allows the Worker to perform both work and eat actions, while the Manager can work and manage without being forced to implement unnecessary methods.
    // Segregated interfaces: IWorkable and IEatable are small and specific, allowing clients to implement only the methods they need.
    // No forced dependencies: The Worker class implements both IWorkable and IEatable, while the Manager class implements IWorkable and a new interface IManageable, avoiding unnecessary methods.
    public class InterfaceSegregationDemo
    {
            public static void Execute()
        {
            IWorkable worker = new Worker();
            worker.Work();

            IEatable eater = new Worker();
            eater.Eat();

            IWorkable manager = new Manager();
            manager.Work();

            IManageable manageable = new Manager();
            manageable.Manage();
        }
    }
}
