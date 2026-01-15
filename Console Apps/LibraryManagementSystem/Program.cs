using LibraryManagementSystem;
using Spectre.Console;



string[] menuChoices = new[] { "View Books", "Add Book", "Delete Book" };


while (true)
{
    Console.Clear();

    MenuOption choice = AnsiConsole.Prompt(
        new SelectionPrompt<MenuOption>()
        .Title("What do you want to do today? ")
        .AddChoices(Enum.GetValues<MenuOption>()));

    switch (choice)
    {
        case MenuOption.ViewBooks:
            BooksController.ViewBooks();
            break;

        case MenuOption.AddBook:

            BooksController.AddBooks();
            break;

        case MenuOption.DeleteBook:
            BooksController.DeleteBooks();
            break;
    }
}

        

    
enum MenuOption
{
    ViewBooks, AddBook, DeleteBook
}
