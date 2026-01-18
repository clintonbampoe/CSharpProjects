namespace CodingTracker.Views;

using CodingTracker.Models;
using Spectre.Console;


class UserInterface
{
    void MainMenu()
    {
        Console.Clear();

        AnsiConsole.MarkupLine("[bold yellow]MAIN MENU[/]");

        Enums.MenuOption choice = AnsiConsole.Prompt(
            new SelectionPrompt<Enums.MenuOption>()
            .Title("What do you want to do today")
            .AddChoices(Enum.GetValues<Enums.MenuOption>()));
        
    }
}