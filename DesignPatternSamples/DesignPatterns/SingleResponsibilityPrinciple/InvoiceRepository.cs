namespace DesignPatterns.SingleResponsibilityPrinciple
{
    // File: InvoiceRepository.cs
    public class InvoiceRepository
    {
        public void Save(Invoice invoice)
        {
            // Simulate saving to a database
            Console.WriteLine($"Invoice {invoice.Id} saved.");
        }
    }
}
