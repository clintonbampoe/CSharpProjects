namespace HabitLogger;
using Microsoft.Data.Sqlite;
using Spectre.Console;

class Program
{
    public static void Main(string[] args)
    {
        string connectionString = "Data Source=Habits.db";
        SqliteConnection connection = new(connectionString);

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