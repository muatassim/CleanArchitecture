using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.OpenClosedPrinciple
{
    // The Open/Closed Principle (OCP) states that software entities (classes, modules, functions, etc.) should be open for extension but closed for modification.
    // This means that the behavior of a module can be extended without modifying its source code, allowing for new functionality to be added without altering existing code.
    // This principle promotes code reusability and maintainability by allowing developers to add new features or behaviors through inheritance or composition, rather than changing existing code.
    // In this example, we have an Invoice class that calculates the total amount based on different discount strategies.
    // The Invoice class is closed for modification (no need to change its code to add new discount types), and the system is open for extension (add new discount strategies by implementing IDiscountStrategy).
    // Open Closed Principle – Open for extension, closed for modification.
    // The IDiscountStrategy interface defines a contract for discount strategies, allowing different implementations to be added without modifying the Invoice class.
    // The NoDiscountStrategy and PercentageDiscountStrategy classes implement this interface, providing specific discount calculations.
    // Extendable without modification : New discount strategies can be added by creating new classes that implement the IDiscountStrategy interface, without changing the Invoice class.
    public class OpenClosedDemo
    {

        public static void Execute()
        {
            //The Invoice class is closed for modification (no need to change its code to add new discount types).	The Invoice class is closed for modification(no need to change its code to add new discount types).
            //The system is open for extension (add new discount strategies by implementing IDiscountStrategy).
            var invoice1 = new Invoice(new NoDiscountStrategy()) { Id = 1, Amount = 100m };
            
            var invoice2 = new Invoice(new PercentageDiscountStrategy(0.1m)) { Id = 2, Amount = 200m };

            Console.WriteLine($"Invoice 1 Total: {invoice1.CalculateTotal():C}");
            Console.WriteLine($"Invoice 2 Total: {invoice2.CalculateTotal():C}");
        }
    }
}
