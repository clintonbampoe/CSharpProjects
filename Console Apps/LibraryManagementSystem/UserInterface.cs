using LibraryManagementSystem;
using Spectre.Console;

class UserInterface
{
    internal static void MainMenu()
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
                    BooksController.ViewBooks();
                    break;

                case Enums.MenuOption.AddBook:

                    BooksController.AddBooks();
                    break;

                case Enums.MenuOption.DeleteBook:
                    BooksController.DeleteBooks();
                    break;
            }
        }

    }
}
