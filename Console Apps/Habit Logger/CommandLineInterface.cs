using HabitLogger;
using Microsoft.Data.Sqlite;
using Spectre.Console;
using System.Globalization;

static class CommandLineInterface 
{ 

    public static void SelectFieldsForEntry(SqliteConnection connection)
    {
        Console.WriteLine("CHOOSE FIELDS TO INSERT");
        Console.WriteLine("\t 1 - Name ,Date and Unit");
        Console.WriteLine("\t 2 - All Fields");
        Console.WriteLine("Press 'q' to return...");
        Console.WriteLine("Enter");


        int value;
        int entryNum = 0;
        do
        {
            if(entryNum > 0)
                InvalidInputErrorMessage();


            Console.Write(">>>");
            string input = Method.Input.Take();

            if (input.ToLower().Equals("q"))
                return;

            while (!(int.TryParse(input, out value)))
            {
                InvalidInputErrorMessage();
                Console.Write(">>>");
                input = Method.Input.Take();
            }

            entryNum++;
        } while (!(value == 1) && !(value == 2));


        if(value == 1)
        {
            Console.WriteLine("Enter Habit: ");
            Console.Write(">>>");
            string name = Method.Input.Take();

            Console.WriteLine("Enter unit: ");
            Console.Write(">>>");
            string unit = Method.Input.Take();

            DateTime date = PromptForDate();
            string sqliteDate = date.ToString("yyyy-MM-dd");

            // INSERT INTO DATABASE
            Table.InsertRow.NameDateAndUnitFields(connection, name, sqliteDate, unit);
        }
        else
        {
            Console.WriteLine("Enter Habit: ");
            Console.Write(">>>");
            string name = Method.Input.Take();

            Console.WriteLine("Enter unit: ");
            Console.Write(">>>");
            string unit = Method.Input.Take();

            DateTime date = PromptForDate();
            string sqliteDate = date.ToString("yyyy-MM-dd");

            Console.WriteLine("Quantity: ");
            Console.Write(">>>");
            string quantityInput = Method.Input.Take();
            int quantity;
            while(!(int.TryParse(quantityInput, out quantity)))
            {
                InvalidInputErrorMessage();
                Console.Write(">>>");
            }

            // INSERT INTO DATABASE
            Table.InsertRow.AllFields(connection, name, sqliteDate, unit, quantity);
            OperationCompletedMessage();

        }
    }

    public static DateTime PromptForDate()
    {
        DateTime parsedDate;
        while(true)
        {
            Console.WriteLine("Enter a date (YYYY-MM-DD)");
            Console.WriteLine("Enter 'today' for today's date");
            Console.Write(">>>");
            string input = Method.Input.Take();

            if (input.ToLower() == "today")
            {
                parsedDate = DateTime.Now;
                return parsedDate;
            }
            else
            {
                // CultureInfo.InvariantCulture ensures parsing is culture neutral
                // DateTimeStyles.None ensures there are no extra parsing rules
                if (DateTime.TryParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                {
                    return parsedDate;
                }
                else
                {
                    InvalidInputErrorMessage();
                    Method.Print.RedText("Please use YYYY-MM-DD");
                }
            }
        }
    }

    public static void InvalidInputErrorMessage()
    {
        Method.Print.RedText("Invalid Input");
        Method.Print.RedText("Try Again");
    }

    public static int EnterRowTo(string command)
    {
        Console.WriteLine($"Enter row to {command}");
        Console.Write(">>>");
        string input = Method.Input.Take();

        int row = ConvertStringToInt(input);

        return row;
    }
     
    public static int ConvertStringToInt(string str)
    {
        int number;

        while (!int.TryParse(str, out number))
        {
            InvalidInputErrorMessage();
            Console.Write(">>>");
            str = Method.Input.Take();
        }

        return number;
    }

    public static bool ConfirmCommand()
    {
        string input = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold yellow]ARE YOU SURE?[/]")
                .AddChoices(
                    "YES",
                    "NO",
                    "< BACK"
                ));

        if (input == "YES")
            return true;
        else
            return false;

    }

    public static void OperationCompletedMessage()
    {
        Method.Print.GreenText("Operation Completed Successfully");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(true);
    }

    public static void WaitForKeyPress()
    {
        Console.WriteLine("\n\n");
        AnsiConsole.Markup("[grey](Press any key to continue...[/])");
        Console.ReadKey(true);
    }

    public static void NoRowsAffectedErrorMessage(string errorDetail)
    {
        Console.WriteLine();
        Method.Formatting.HorizontalLine(20);
        Method.Print.RedText("ERROR");
        Method.Print.RedText($"0 rows affected! - {errorDetail}");
    }

    public static void InsertDataIntoHabitDateAndUnitFields(SqliteConnection connection)
    {
        // insert habit, date and unit only
        Console.WriteLine("Enter Habit: ");
        Console.Write(">>>");
        string name = Method.Input.Take();

        Console.WriteLine("Enter unit: ");
        Console.Write(">>>");
        string unit = Method.Input.Take();

        DateTime date = CommandLineInterface.PromptForDate();
        string sqliteDate = date.ToString("yyyy-MM-dd");

        // INSERT INTO DATABASE
        Table.InsertRow.NameDateAndUnitFields(connection, name, sqliteDate, unit);
    }

    public static void InsertDataIntoAllFields(SqliteConnection connection)
    {
        // insert all fields
        Console.WriteLine("Enter Habit: ");
        Console.Write(">>>");
        string name = Method.Input.Take();

        Console.WriteLine("Enter unit: ");
        Console.Write(">>>");
        string unit = Method.Input.Take();

        DateTime date = CommandLineInterface.PromptForDate();
        string sqliteDate = date.ToString("yyyy-MM-dd");

        Console.WriteLine("Quantity: ");
        Console.Write(">>>");
        string quantityInput = Method.Input.Take();
        int quantity;
        while (!(int.TryParse(quantityInput, out quantity)))
        {
            CommandLineInterface.InvalidInputErrorMessage();
            Console.Write(">>>");
        }

        // INSERT INTO DATABASE
        Table.InsertRow.AllFields(connection, name, sqliteDate, unit, quantity);
        CommandLineInterface.OperationCompletedMessage();
    }

    public static void TableDoesnotExistErrorMessage(SqliteException exception)
    {
        Console.WriteLine(exception.Message);
        AnsiConsole.MarkupLine("Create a [bold yellow]NEW TABLE[/] first!");
        WaitForKeyPress();
    }
}
