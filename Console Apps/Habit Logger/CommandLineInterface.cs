using HabitLogger;
using Microsoft.Data.Sqlite;
using SQLitePCL;
using System.Globalization;

class CommandLineInterface 
{ 

    public static void SelectFieldsForEntry(SqliteConnection connection)
    {
        Console.WriteLine("Pick Fields");
        Console.WriteLine("\t 1 - Name & Date only");
        Console.WriteLine("\t 2 - Name , Date and Occurances only");
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

            DateTime date = PromptForDate();
            string sqliteDate = date.ToString("yyyy-MM-dd");

            // INSERT INTO DATABASE
            Table.InsertRow.NameAndDateOnly(connection, name, sqliteDate);
        }
        else
        {
            Console.WriteLine("Enter Habit: ");
            Console.Write(">>>");
            string name = Method.Input.Take();

            DateTime date = PromptForDate();
            string sqliteDate = date.ToString("yyyy-MM-dd");

            Console.WriteLine("Occurances: ");
            Console.Write(">>>");

            string occurancesInput = Method.Input.Take();
            int occurances;
            while(!(int.TryParse(occurancesInput, out occurances)))
            {
                InvalidInputErrorMessage();
                Console.Write(">>>");
            }

            // INSERT INTO DATABASE
            Table.InsertRow.New(connection, name, sqliteDate, occurances);
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
        bool validInput = int.TryParse(str, out number);

        while (!validInput)
        {
            InvalidInputErrorMessage();
            Console.Write(">>>");
        }

        return number;
    }

    public static bool ConfirmCommand()
    {
        Method.Print.RedText("Are you sure you to continue...");
        Console.WriteLine("Enter 'y' for YES and any other key for NO");
        Console.Write(">>>");
        string input = Method.Input.Take();

        if (input.Contains("y") || input.Contains("Y"))
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
}
