using Spectre.Console;
namespace LibraryManagementSystem;

internal  class booksController
{

    internal void ViewBooks()
    {
        AnsiConsole.MarkupLine("[bold yellow]List of Books:[/]");

        foreach (var book in MockDatabase.Books)
        {
            AnsiConsole.MarkupLine($"- [cyan]{book}[/]");
        }

        AnsiConsole.MarkupLine("Press Any Key to Continue.");
        Console.ReadKey(true);
    }

    internal void AddBooks()
    {
        string title = AnsiConsole.Ask<string>("Enter the [green]title[/] of the book you want to add: ");

        if (MockDatabase.Books.Contains(title))
        {
            AnsiConsole.MarkupLine("[red]This book already exists.[/]");
        }
        else
        {
            MockDatabase.Books.Add(title);
            AnsiConsole.MarkupLine("[green]Book added successfully![/]");
        }

        AnsiConsole.MarkupLine("Press Any Key to Continue.");
        Console.ReadKey(true);
    }

    internal void DeleteBooks()
    {
        if (MockDatabase.Books.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No books available to delete.[/]");
            Console.ReadKey(true);
            return;
        }

        string bookToDelete = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Select a [red]book[/] to delete:")
            .AddChoices(MockDatabase.Books));

        if (MockDatabase.Books.Remove(bookToDelete))
        {
            AnsiConsole.MarkupLine("[red]Book deleted successfully![/]");
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Book not found.[/]");
        }

        AnsiConsole.MarkupLine("Press Any Key to Continue.");
        Console.ReadKey(true);
    }
}