using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Mediatr
{

    // Usage Example
    public class MediatrDemo
    {
        public static void Execute()
        {
            var mediator = new SimpleMediator();

            // Command
            SimpleMediator.Send(new CreateUserCommand { UserName = "Alice" });

            // Query
            var result = SimpleMediator.Query<GetUserQuery, string>(new GetUserQuery { UserId = 1 });
            Console.WriteLine(result);
        }
    }
}
