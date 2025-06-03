namespace DesignPatterns.SingleResponsibilityPrinciple
{
    // File: Invoice.cs
    public class Invoice
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }

        public decimal CalculateTotal()
        {
            // Example calculation logic
            return Amount * 1.2m; // e.g., adding 20% tax
        }
    }
}
