using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.LiskovSubstitutionPrinciple
{
    // The Liskov Substitution Principle (LSP) states that objects of a superclass should be replaceable with objects of a subclass without affecting the correctness of the program.
    // This means that a derived class should extend the behavior of the base class without changing its expected behavior.
    // In this example, we have a base class Shape with a method GetArea(), and two derived classes: Rectangle and Square.
    // Both derived classes implement the GetArea() method, allowing them to be used interchangeably as Shapes.
    // The Rectangle class has Width and Height properties, while the Square class has a single Side property.
    // The Liskov Substitution Principle ensures that both Rectangle and Square can be used wherever a Shape is expected, without breaking the functionality of the program.
    // Liskov Substitution Principle – Objects of a superclass should be replaceable with objects of a subclass without affecting the correctness of the program.
    // Subtypes replaces base types without breaking functionality:
    public class LiskovDemo
    {
        /// <summary>
        /// Both Rectangle and Square can be used wherever a Shape is expected.
        /// The program works correctly regardless of which derived class is used, as both provide a valid implementation of GetArea().
        /// </summary>
        public static void Execute()
        {
            List<Shape> shapes =
            [
                new Rectangle { Width = 4, Height = 5 },
                new Square { Side = 3 }
            ];

            foreach (var shape in shapes)
            {
                Console.WriteLine($"Area: {shape.GetArea()}");
            }
        }
    }
}
