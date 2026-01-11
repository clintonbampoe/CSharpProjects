namespace HabitLogger;
using Microsoft.Data.Sqlite;

class Program
{
    public static void Main(string[] args)
    {
        string connectionString = "Data Source=Habits.db";
        SqliteConnection connection = new(connectionString);

        while (true)
        {
            // HEADER
            Console.Title = "THE HABIT LOGGER";


            Console.Clear();
            Method.Formatting.HorizontalLine(50);
            Console.WriteLine();


            Console.WriteLine("WELCOME TO HABITS");

            Console.WriteLine("--- Row Management ---");
            Console.WriteLine("\t i - Insert Row");
            Console.WriteLine("\t u - Update Row");
            Console.WriteLine("\t d - Delete Row");
            Console.WriteLine("\t a - View All");

            Console.WriteLine();
            Console.WriteLine("--- Table Management ---");
            Console.WriteLine("\t t - New Table");
            Console.WriteLine("\t x - Delete Table");

            Console.WriteLine("press 'q' to quit...");

            Console.WriteLine("Enter Command...");


            bool ValidInput;
            string input = "";
            do
            {
                Console.Write(">>> ");
                input = Method.Input.Take();

                switch (input.ToLower())
                {
                    case "q":
                        return;

                    case "i":
                    case "u":
                    case "d":
                    case "a":
                    case "t":
                    case "x":
                        ValidInput = true;
                        break;

                    default:
                        CommandLineInterface.InvalidInputErrorMessage();
                        ValidInput = false;
                        break;
                }
            } while (!ValidInput);


            using (connection)
            {
                connection.Open();
                Table.Create.NewTable(connection);

                //
                if (input.ToLower() == "i")
                { 
                    CommandLineInterface.SelectFieldsForEntry(connection);
                    CommandLineInterface.OperationCompletedMessage();
                }

                else if(input.ToLower() == "u")
                {
                    int row = CommandLineInterface.EnterRowTo("Update");

                    Console.WriteLine("Occurances: ");
                    Console.WriteLine("Enter quantity");
                    Console.Write(">>>");

                    string userInput = Method.Input.Take();
                    int quantity = CommandLineInterface.ConvertStringToInt(userInput);

                    Table.Update.Row(connection, row, quantity);
                    CommandLineInterface.OperationCompletedMessage();
                }

                else if(input.ToLower() == "d")
                {
                    int row = CommandLineInterface.EnterRowTo("Delete");

                    if (CommandLineInterface.ConfirmCommand())
                    {
                        Table.Delete.Row(connection, row);
                        CommandLineInterface.OperationCompletedMessage();
                    }
                    else
                        break;
                }

                else if (input.ToLower() == "a")
                {
                    Table.Display.All(connection);

                    Console.WriteLine("Press key close to close...");
                    Console.ReadKey(true);
                }
                
                else if(input.ToLower() == "t")
                {
                    Table.Create.NewTable(connection);
                    CommandLineInterface.OperationCompletedMessage();
                }
                
                else if (input.ToLower() == "x")
                {
                    
                    if (CommandLineInterface.ConfirmCommand())
                    {
                        Table.Delete.Table(connection);
                        CommandLineInterface.OperationCompletedMessage();
                    }
                    else
                        break;
                }
            }
        }
    }
}