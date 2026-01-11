using HabitLogger;
using Microsoft.Data.Sqlite;
using System.Globalization;
using System.Reflection.Metadata;

class CommandLineInterface 
{ 
    public static void Run()
    {
        Console.Title = "THE HABIT LOGGER";
        Console.WriteLine("Press any key to Proceed...");
        Console.ReadKey();

        Console.Clear();
        Method.Formatting.HorizontalLine(50);
        Console.WriteLine();

        Console.WriteLine("WELCOME TO HABITS");
        Console.WriteLine("\t n - New ");
        Console.WriteLine("\t a - View All");
        Console.WriteLine("\t u - Update");
        Console.WriteLine("\t d - Delete");
        Console.WriteLine("Enter");


        bool ValidInput;
        string input;
        do
        {
            Console.Write(">>> ");
            input = Method.Input.Take();

            switch (input.ToLower())
            {
                case "n":
                case "a":
                case "u":
                case "d":
                    ValidInput = true;
                    break;
                
                default:
                    Method.Print.RedText("Invalid Input!!!");
                    Method.Print.RedText("Try Again!");
                    ValidInput = false;
                    break;
            }
        } while (!ValidInput);


        //
        if (input.ToLower() == "n")
        {
           

        }




    }
    public static void SelectFieldsForEntry(SqliteConnection connection)
    {
        Console.WriteLine("Pick Fields");
        Console.WriteLine("\t 1 - Name & Date only");
        Console.WriteLine("\t 2 - Name , Date and Occurances only");
        Console.WriteLine("Press 'q' to return...");
        Console.WriteLine("Enter");
        Console.Write(">>>");
        
        
        int value;
        string input = Method.Input.Take();
        bool validInput = int.TryParse(input, out value);


        if (input.ToLower() == "q")
            return;


        if (!(validInput && (value == 1 || value == 2)))
        {
            InvalidInputDisplayMessage();
            Console.Write(">>>");
        }


        if(value == 1)
        {
            Console.WriteLine("Name: ");
            Console.Write(">>>");
            string name = Method.Input.Take();

            DateTime date = PromptForDate();
            string sqliteDate = date.ToString("yyyy-MM-dd");

            // INSERT INTO DATABASE
            Table.InsertRow.NameAndDateOnly(connection, name, sqliteDate);
        }
        else
        {
            Console.WriteLine("Name: ");
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
                InvalidInputDisplayMessage();
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
            Console.Write(">>>");
            string input = Method.Input.Take();

            // CultureInfo.InvariantCulture ensures parsing is culture neutral
            // DateTimeStyles.None ensures there are no extra parsing rules
            if(DateTime.TryParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
            {
                return parsedDate;
            }
            else
            {
                InvalidInputDisplayMessage();
                Method.Print.RedText("Please use YYYY-MM-DD");
            }
        }
    }

    public static void InvalidInputDisplayMessage()
    {
        Method.Print.RedText("Invalid Input");
        Method.Print.RedText("Try Again");
    }
}
