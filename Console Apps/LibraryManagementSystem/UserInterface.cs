using LibraryManagementSystem.Controllers;
using Spectre.Console;
using System.Runtime.CompilerServices;

class UserInterface
{
    private BooksController booksController = new();
    internal void MainMenu()
    {
        while (true)
        {
            Console.Clear();

            Enums.MenuOption choice = AnsiConsole.Prompt(
                new SelectionPrompt<Enums.MenuOption>()
                .Title("What do you want to do today? ")
                .AddChoices(Enum.GetValues<Enums.MenuOption>()));

            switch (choice)
            {
                case Enums.MenuOption.ViewBooks:
                    booksController.ViewItems();
                    break;

                case Enums.MenuOption.AddBook:

                    booksController.AddItem();
                    break;

                case Enums.MenuOption.DeleteBook:
                    booksController.DeleteItem();
                    break;
            }
        }

    }
}
