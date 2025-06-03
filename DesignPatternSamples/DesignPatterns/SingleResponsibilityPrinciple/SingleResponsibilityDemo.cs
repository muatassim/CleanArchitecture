using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.SingleResponsibilityPrinciple
{
    // The Single Responsibility Principle (SRP) states that a class should have only one reason to change, meaning it should only have one job or responsibility.
    // This principle helps to create classes that are easier to understand, maintain, and test by ensuring that each class is focused on a single task or functionality.
    // In this example, we have three classes: Invoice, InvoicePrinter, and InvoiceRepository. Each class has a single responsibility:
    // Invoice handles invoice data and calculation logic, InvoicePrinter handles printing logic, and InvoiceRepository handles data storage logic.
    // Single Responsibility Principle – One class, one job.
    public class SingleResponsibilityDemo
    {
        public static void Execute()
        {
            //•	Invoice handles invoice data and calculation logic only.    
            var invoice = new Invoice { Id = 1, Amount = 100m };
            //•	InvoicePrinter handles printing logic only.
            var printer = new InvoicePrinter();
            //•	InvoiceRepository handles data storage logic only.
            var repository = new InvoiceRepository();

            printer.Print(invoice);
            repository.Save(invoice);
        }
    }
}
