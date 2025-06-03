namespace DesignPatterns.LiskovSubstitutionPrinciple
{
    public class Rectangle : Shape
    {
        public decimal Width { get; set; }
        public decimal Height { get; set; }

        public override decimal GetArea() => Width * Height;
    }
}
