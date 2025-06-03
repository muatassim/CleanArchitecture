namespace DesignPatterns.OpenClosedPrinciple
{
    public class NoDiscountStrategy : IDiscountStrategy
    {
        public decimal ApplyDiscount(decimal amount) => amount;
    }
}
