using MathGame;

class Method
{
    public static string TakeInput()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        string input = Console.ReadLine() ?? "";
        Console.ForegroundColor = ConsoleColor.White;
        return input;
    }

    public static void HorizontalLine(int length)
    {
        for(int i = 0; i < length; i++)
        {
            Console.Write('-');
        }
        Console.WriteLine();
    }
}