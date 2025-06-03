namespace DesignPatterns.OpenClosedPrinciple
{
    public class Invoice(IDiscountStrategy discountStrategy)
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        private readonly IDiscountStrategy _discountStrategy = discountStrategy;

        public decimal CalculateTotal()
        {
            return _discountStrategy.ApplyDiscount(Amount);
        }
    }
}
