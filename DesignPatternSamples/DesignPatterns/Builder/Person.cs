namespace DesignPatterns.Builder
{
    public class Person
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int Age { get; set; }
        public string Address { get; set; } = null!;

        public override string ToString() =>
            $"{FirstName} {LastName}, Age: {Age}, Address: {Address}";
    }
}
