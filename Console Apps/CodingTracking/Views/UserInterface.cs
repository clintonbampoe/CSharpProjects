namespace CodingTracker.Views;

using CodingTracker.Models;
using Spectre.Console;


class UserInterface
{
    public Enums.MenuOption MainMenu()
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[bold yellow]MAIN MENU[/]");

        Enums.MenuOption choice = AnsiConsole.Prompt(
            new SelectionPrompt<Enums.MenuOption>()
            .Title("What do you want to do today")
            .AddChoices(Enum.GetValues<Enums.MenuOption>()));

        return choice;
    }

    public CodingSession AddSession()
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[bold]Add a New Session[/]");

        DateTime startTime = AnsiConsole.Ask<DateTime>("Enter the [bold blue]Start Time[/] of this session: ");
        DateTime endTime = AnsiConsole.Ask<DateTime>("Enter the [bold blue]End Time[/] of this session: ");

        return new CodingSession(startTime, endTime);
    }

    public CodingSession EditSession()
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[bold]Edit Session[/]");

        int sessionId = AnsiConsole.Ask<int>("Enter the [blue]Id[/] of the session you want to edit: ");
        DateTime startTime = AnsiConsole.Ask<DateTime>("End the new [blue]Start Time[/] of the session: ");
        DateTime endTime = AnsiConsole.Ask<DateTime>("Enter the new [blue]End Time[/] of this session: ");

        return new CodingSession(sessionId, startTime, endTime);
    }

    public int DeleteSession()
    {
        Console.Clear();
        AnsiConsole.MarkupLine("[bold]Delete Session[/]");

        return AnsiConsole.Ask<int>("Enter the [blue]Id[/] of the session you want to delete: ");
    }

    public void ViewAllSessions(IEnumerable<CodingSession> allSessions)
    {
        var table = new Table()
            .AddColumn("Id")
            .AddColumn("Start Time")
            .AddColumn("End Time")
            .AddColumn("Duration");
        {
            table.Border = TableBorder.Rounded;
        }

        foreach (CodingSession session in allSessions)
        {
            table.AddRow(
                session.Id.ToString(),
                session.StartTime.ToString("dddd, MMM d, yyyy h:mm tt"),
                session.EndTime.ToString("dddd, MMM d, yyyy h:mm tt"),
                session.Duration.ToString());
        }

        AnsiConsole.Write(table);
        Console.WriteLine("\n");

        WaitForKeyPress();
    }

    public void PrintOperationSuccessfulMessage(string message)
    {
        Console.WriteLine();
        AnsiConsole.MarkupLine("[green]Operation completed successfully![/]");
        AnsiConsole.MarkupLine($"[blue]{message}:[/] 1 row(s) affected");
        WaitForKeyPress();
    }

    public void PrintOperationFailedMessage()
    {
        Console.WriteLine();
        AnsiConsole.MarkupLine("[red]Operation failed[/]");
        AnsiConsole.MarkupLine("Target [yellow]row[/] does not exist!");
        AnsiConsole.MarkupLine($"0 rows(s) affected!");
        WaitForKeyPress();
    }

    public void WaitForKeyPress()
    {
        AnsiConsole.MarkupLine("[grey](Press any [blue]key[/] to Continue...)[/]");
        Console.ReadKey(true);
    }

    public void InvalidInput(string message)
    {
        Console.WriteLine();
        AnsiConsole.MarkupLine("[bold red]Invalid Input![/]");
        AnsiConsole.MarkupLine($"[red]Error:[/] {message}");
        AnsiConsole.MarkupLine("[red]Try Again[/]");
        WaitForKeyPress();
    }
}