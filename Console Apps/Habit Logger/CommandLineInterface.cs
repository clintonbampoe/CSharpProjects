using HabitLogger;
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

        Console.WriteLine("Welcome to THE HABIT LOGGER");
        Console.WriteLine("\t n - Create New Habit");
        Console.WriteLine("\t u - Update Existing Habit");

        Console.WriteLine("Enter");
        Console.Write(">>> ");
        string input = Method.Input.Take();

        while (!(input.ToLower() == "n") && !(input.ToLower() == "u"))
        {
            Method.Print.RedText("Invalid Input");
            Method.Print.RedText("Please Try Again");
            Console.Write(">>> ");
            input = Method.Input.Take();
        }

        if(input.ToLower() == "n")
        {
            // create new habit in database
        }
        else
        {
            // perform C.R.U.D operations on existing habit
        }

    }
}
