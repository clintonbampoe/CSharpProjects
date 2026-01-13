namespace HabitLogger;
using Microsoft.Data.Sqlite;
using Spectre.Console;

class Program
{
    public static void Main(string[] args)
    {
        // set app state
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        string connectionString = "Data Source=Habits.db";
        SqliteConnection connection = new(connectionString);

        AnsiConsole.MarkupLine("[bold yellow]LOADING HABIT LOGGER![/]");

        AnsiConsole.Status()
            .Spinner(Spinner.Known.Arc)
            .Start("Connecting to database...", ctx =>
            {
                // Simulate some work
                Thread.Sleep(1000);

                ctx.Status("Loading app assets...");
                Thread.Sleep(2000);

                ctx.Status("Allocating system resources!");
                Thread.Sleep(1000);
            });

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine("[bold green]READY![/]");
        CommandLineInterface.WaitForKeyPress();

        using (connection)
        {
            connection.Open();

            while (true)
            {
                Console.Clear();
                // MAIN MENU
                string input = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[bold yellow]MAIN MENU[/]")
                        .AddChoices(
                            "MANAGE ROWS",
                            "MANAGE TABLES",
                            "EXIT"
                        ));

                if (input == "MANAGE ROWS")
                {
                    Menu.RowManagement(connection);
                }
                else if (input == "MANAGE TABLES")
                {
                    Menu.TableManagement(connection);
                }
                else return;
            }
        }
    }
}