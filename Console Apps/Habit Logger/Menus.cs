using Spectre.Console;
using HabitLogger;
using Microsoft.Data.Sqlite;

public class Menu
{
    // ROW MANAGEMENT
    public static void RowManagement(SqliteConnection connection)
    {
        string input = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold yellow]ROW MANAGEMENT MENU[/]")
                .AddChoices(
                    "INSERT ROW",
                    "EDIT ROW",
                    "DELETE ROW",
                    "VIEW ALL ROWS",
                    "BACK TO MAIN MENU"
                ));

        if (input == "INSERT ROW")
        {
            InsertRow(connection);
        }
        else if (input == "EDIT ROW")
        {
            UpdateRow(connection);
        }
        else if (input == "DELETE ROW")
        {
            DeleteRow(connection);
        }
        else if (input == "VIEW ALL ROWS")
        {
            ViewAllRows(connection);
        }
        else
            return;
    }

    public static void InsertRow(SqliteConnection connection)
    {

        string input = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold yellow]SELECT FIELDS TO INSERT[/]")
                .AddChoices(
                    "HABIT, DATE AND UNIT ONLY",
                    "ALL FIELDS",
                    "< BACK"
                ));

        if (input == "HABIT, DATE AND UNIT ONLY")
        {
            // insert habit, date and unit only
           
        }
        else if (input == "ALL FIELDS")
        {
            // insert all fields
            
        }
    }

    public static void UpdateRow(SqliteConnection connection)
    {
        // prompt for row number
        int row = CommandLineInterface.EnterRowTo("Update");

        string input = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold yellow]SELECT FIELDS TO UPDATE[/]")
                .AddChoices(
                    "UNIT",
                    "QUANTITY",
                    "< BACK"
                ));

        if (input == "UNIT")
        {
            // update unit only
            Console.WriteLine("UNIT ");
            Console.WriteLine("Enter new unit");
            Console.Write(">>>");

            string userInput = Method.Input.Take();

            Table.UpdateRow.Unit(connection, row, userInput);
        }
        else if (input == "QUANTITY")
        {
            // update quantity only
            Console.WriteLine("QUANTITY ");
            Console.WriteLine("Enter new quantity");
            Console.Write(">>>");

            string userInput = Method.Input.Take();
            int quantity = CommandLineInterface.ConvertStringToInt(userInput);

            Table.UpdateRow.Quantity(connection, row, quantity);
        }
    }

    public static void DeleteRow(SqliteConnection connection)
    {
        // prompt for row number


        string input = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold yellow]ARE YOU SURE?[/]")
                .AddChoices(
                    "[red]YES[/]",
                    "[green]NO[/]",
                    "< BACK"
                ));

        if (input == "[red]YES[/]")
        {
            // delete row
        }
        else
            return;
    }

    public static void ViewAllRows(SqliteConnection connection)
    {
        Table.Display.AllRows(connection);
    }


    // TABLE MANAGEMENT
    public static void TableManagement(SqliteConnection connection)
    {
        string input = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold yellow]TABLE MANAGEMENT MENU[/]")
                .AddChoices(
                    "NEW TABLE",
                    "TABLE INFO",
                    "DELETE TABLE",
                    "BACK TO MAIN MENU"
                ));

        if (input == "NEW TABLE")
        {
            NewTable(connection);
        }
        else if (input == "TABLE INFO")
        {
            TableInfo(connection);
        }
        else if (input == "DELETE TABLE")
        {
            DeleteTable(connection);
        }
        else
            return;
    }

    public static void NewTable(SqliteConnection connection)
    {

    }

    public static void DeleteTable(SqliteConnection connection)
    {
        string input = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold yellow]ARE YOU SURE?[/]")
                .AddChoices(
                    "[red]YES[/]",
                    "[green]NO[/]",
                    "< BACK"
                ));
    }

    public static void TableInfo(SqliteConnection connection)
    {

    }
}


