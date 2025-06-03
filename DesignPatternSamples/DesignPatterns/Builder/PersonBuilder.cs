namespace DesignPatterns.Builder
{
    public class PersonBuilder
    {
        private readonly Person _person = new();

        public PersonBuilder SetFirstName(string firstName)
        {
            _person.FirstName = firstName;
            return this;
        }

        public PersonBuilder SetLastName(string lastName)
        {
            _person.LastName = lastName;
            return this;
        }

        public PersonBuilder SetAge(int age)
        {
            _person.Age = age;
            return this;
        }

        public PersonBuilder SetAddress(string address)
        {
            _person.Address = address;
            return this;
        }

        public Person Build() => _person;
    }
}
