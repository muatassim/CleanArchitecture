namespace DesignPatterns.LiskovSubstitutionPrinciple
{
    public class Square : Shape
    {
        public decimal Side { get; set; }

        public override decimal GetArea() => Side * Side;
    }
}
