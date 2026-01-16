using LibraryManagementSystem;

// UserInterface.MainMenu();

var car1 = new Car();
var car2 = new Car();

car1.name = "Ferrrari";
car2.name = "BMW";

car1.PrintName();
car2.PrintName();

class Car
{
    internal string name = "";

    internal void PrintName()
    {
        name = name.ToUpper();
        Console.WriteLine($"I'm a {name}");
    }
}