namespace DesignPatterns.SingleResponsibilityPrinciple
{
    // File: InvoicePrinter.cs
    public class InvoicePrinter
    {
        public void Print(Invoice invoice)
        {
            Console.WriteLine($"Invoice Id: {invoice.Id}, Total: {invoice.CalculateTotal():C}");
        }
    }
}
