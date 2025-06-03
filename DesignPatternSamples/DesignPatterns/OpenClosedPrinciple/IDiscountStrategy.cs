namespace DesignPatterns.OpenClosedPrinciple
{
    public interface IDiscountStrategy
    {
        decimal ApplyDiscount(decimal amount);
    }
}
