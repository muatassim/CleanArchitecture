using Spectre.Console;
using DesignPatterns.OpenClosedPrinciple;
using DesignPatterns.LiskovSubstitutionPrinciple;
using DesignPatterns.InterfaceSegragationPrinciple;
using DesignPatterns.DependencyInjection;
using DesignPatterns.SingleResponsibilityPrinciple;
using DesignPatterns.Builder;
using DesignPatterns.FactoryPattern;
using DesignPatterns.Mediatr;

AnsiConsole.Write(
    new FigletText("Design Patterns")
        .Centered()
        .Color(Color.Cyan1)
);

AnsiConsole.Write(
    new Rule("[yellow]C# Principles Showcase[/]").RuleStyle("grey").Centered()
);

AnsiConsole.MarkupLine("[bold green]Open Closed Principle Example[/]");
OpenClosedDemo.Execute();

AnsiConsole.MarkupLine("[bold green]Liskov Substitution Principle Example[/]");
LiskovDemo.Execute();

AnsiConsole.MarkupLine("[bold green]Interface Segregation Principle Example[/]");
InterfaceSegregationDemo.Execute();

AnsiConsole.MarkupLine("[bold green]Dependency Inversion Principle Example[/]");
DependencyInversionDemo.Execute();

AnsiConsole.MarkupLine("[bold green]Single Responsibility Principle Example[/]");
SingleResponsibilityDemo.Execute();



AnsiConsole.MarkupLine("[bold green]Single Builder Principle Example[/]");
BuilderDemo.Execute();


AnsiConsole.MarkupLine("[bold green]Single Factory Pattern Example[/]");
FactorDemo.Execute();


AnsiConsole.MarkupLine("[bold green]Single Mediatr Example[/]");
MediatrDemo.Execute();


AnsiConsole.Write(
    new Panel("[bold yellow]Press any key to exit...[/]")
        .Border(BoxBorder.Double)
        .BorderStyle(new Style(Color.Blue))
        .Padding(1, 1) 
);


 

Console.ReadKey(true);

AnsiConsole.Write(
    new Panel("[bold green]Exiting the application...[/]\n[grey]Application exited successfully.[/]\n[italic]Thank you for using the Design Patterns application![/]")
        .Border(BoxBorder.Rounded)
        .BorderStyle(new Style(Color.Green))
        .Padding(1, 1) 
);


Console.WriteLine("Application exited successfully. Thank you for using the Design Patterns application!");
