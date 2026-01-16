using Spectre.Console;
namespace LibraryManagementSystem;

internal  class BooksController
{

    internal void ViewBooks()
    {
        Table table = new();
        table.Border(TableBorder.Rounded);

        table.AddColumn("[yellow]Id[/]");
        table.AddColumn("[yellow]Title[/]");
        table.AddColumn("[yellow]Author[/]");
        table.AddColumn("[yellow]Category[/]");
        table.AddColumn("[yellow]Location[/]");
        table.AddColumn("[yellow]Pages[/]");

        IEnumerable<Book> books = MockDatabase.LibraryItems.OfType<Book>();
        foreach (Book book in books)
        {
            table.AddRow(
                book.Id.ToString(),
                $"[cyan]{book.Name}[/]",
                $"[cyan]{book.Author}[/]",
                $"[cyan]{book.Category}[/]",
                $"[cyan]{book.Location}[/]",
                book.Pages.ToString()
            );
        }

        AnsiConsole.Write(table);
        AnsiConsole.Markup("Press Any Key To Continue.");
        Console.ReadKey(true);

    }

    internal void AddBooks()
    {
        var title = AnsiConsole.Ask<string>("Enter the [green]title[/] of the book to add:");
        var author = AnsiConsole.Ask<string>("Enter the [green]author[/] of the book:");
        var category = AnsiConsole.Ask<string>("Enter the [green]category[/] of the book:");
        var location = AnsiConsole.Ask<string>("Enter the [green]location[/] of the book:");
        var pages = AnsiConsole.Ask<int>("Enter the [green]number of pages[/] in the book:");

        if (MockDatabase.LibraryItems.OfType<Book>().Any(b => b.Name.Equals(title, StringComparison.OrdinalIgnoreCase)))
        {
            AnsiConsole.MarkupLine("[red]This book already exists.[/]");
        }
        else
        {
            Book newBook = new(MockDatabase.LibraryItems.Count + 1, title, author, category, location, pages);
            MockDatabase.LibraryItems.Add(newBook);
            AnsiConsole.MarkupLine("[green]Book added successfully![/]");
        }

        AnsiConsole.MarkupLine("Press Any Key to Continue.");
        Console.ReadKey(true);
    }

    internal void DeleteBooks()
    {
        IEnumerable<Book> books = MockDatabase.LibraryItems.OfType<Book>();

        if (!books.Any())
        {
            AnsiConsole.MarkupLine("[red]No books available to delete.[/]");
            Console.ReadKey(true);
            return;
        }

        LibraryItem bookToDelete = AnsiConsole.Prompt(
            new SelectionPrompt<LibraryItem>()
            .Title("Select a [red]book[/] to delete:")
            .UseConverter(b => b.Name)
            .AddChoices(books));

        if (MockDatabase.LibraryItems.Remove(bookToDelete))
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