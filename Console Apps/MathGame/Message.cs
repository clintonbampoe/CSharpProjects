using MathGame;

class Message
{
    public static void GreenText(string text)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(text);
        Console.ForegroundColor = ConsoleColor.White;
    }

    public static void RedText(string text)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(text);
        Console.ForegroundColor = ConsoleColor.White;
    }
}