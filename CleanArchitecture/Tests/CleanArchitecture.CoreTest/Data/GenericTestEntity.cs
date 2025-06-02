using CleanArchitecture.Core.Abstractions;

namespace CleanArchitecture.CoreTest.Data
{
    internal class GenericTestEntity : Entity
    {
        public string Name { get; set; } = string.Empty;
        public string Age { get; set; } = string.Empty;
    }
}