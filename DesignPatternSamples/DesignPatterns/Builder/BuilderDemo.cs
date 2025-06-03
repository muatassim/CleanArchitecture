using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Builder
{




    public class BuilderDemo
    {
        public static void Execute()
        {
            var person = new PersonBuilder()
                .SetFirstName("John")
                .SetLastName("Doe")
                .SetAge(30)
                .SetAddress("123 Main St")
                .Build();

            Console.WriteLine($"Built Person: {person}");
        }
    }
}
