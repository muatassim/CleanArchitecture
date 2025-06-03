using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.DependencyInjection
{
    // The Dependency Inversion Principle (DIP) states that high-level modules should not depend on low-level modules, but both should depend on abstractions.
    // Additionally, abstractions should not depend on details; details should depend on abstractions.
    // This principle helps to reduce the coupling between high-level and low-level modules, making the system more flexible and easier to maintain.
    //Dependency Inversion Principle – Depend on abstractions, not concretions. 
    public class DependencyInversionDemo
    {
        public static void Execute()
        {
            var emailService = new NotificationService(new EmailSender());
            emailService.Notify("Hello via Email!");

            var smsService = new NotificationService(new SmsSender());
            smsService.Notify("Hello via SMS!");
        }
    }
}
