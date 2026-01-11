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
            Console.WriteLine("\t h - Table Info");
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
                    case "h":
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

                //
                if (input.ToLower() == "i")
                { 
                    CommandLineInterface.SelectFieldsForEntry(connection);
                }

                else if(input.ToLower() == "u")
                {
                    Console.Clear();
                    int row = CommandLineInterface.EnterRowTo("Update");

                    Console.WriteLine("CHOOSE FIELD TO UPDATE");
                    Console.WriteLine("\t u - Unit");
                    Console.WriteLine("\t q - Quantity");
                    Console.Write(">>>");
                    string updateFieldInput = Method.Input.Take();

                    if (updateFieldInput.ToLower().Equals("u"))
                    {
                        Console.WriteLine("UNIT ");
                        Console.WriteLine("Enter new unit");
                        Console.Write(">>>");

                        string userInput = Method.Input.Take();

                        Table.UpdateRow.Unit(connection, row, userInput);
                    }
                    else if (updateFieldInput.ToLower().Equals("q"))
                    {
                        Console.WriteLine("QUANTITY ");
                        Console.WriteLine("Enter new quantity");
                        Console.Write(">>>");

                        string userInput = Method.Input.Take();
                        int quantity = CommandLineInterface.ConvertStringToInt(userInput);

                        Table.UpdateRow.Quantity(connection, row, quantity);
                    }
                    else
                    {
                        Method.Print.RedText("Invalid Input");
                        break;
                    }

                }

                else if(input.ToLower() == "d")
                {
                    int row = CommandLineInterface.EnterRowTo("Delete");

                    if (CommandLineInterface.ConfirmCommand())
                    {
                        Table.Delete.Row(connection, row);
                    }
                    else
                        break;
                }

                else if (input.ToLower() == "a")
                {
                    Table.Display.AllRows(connection);

                    Console.WriteLine("Press key close to close...");
                    Console.ReadKey(true);
                }
                
                else if(input.ToLower() == "t")
                {
                    Table.Create.NewTable(connection);
                }
                
                else if (input.ToLower() == "x")
                {
                    
                    if (CommandLineInterface.ConfirmCommand())
                    {
                        Table.Delete.Table(connection);
                    }
                    else
                        break;
                }
                else if((input.ToLower() == "h"))
                {
                    Table.Display.TableInfo(connection);
                    CommandLineInterface.OperationCompletedMessage();
                }
            }
        }
    }
}