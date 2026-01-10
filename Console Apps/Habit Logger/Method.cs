using HabitLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Method
{
    public class Input
    {
        public static string Take()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            string input = Console.ReadLine() ?? "";
            Console.ForegroundColor = ConsoleColor.White;
            return input;
        }

        public static void WaitForKeyPress()
        {
            Console.WriteLine("Press any key to proceed...");
            Console.ReadKey();
        }
    }
    public class Print
    {
        public static void RedText(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void GreenText(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    public class Formatting
    {
        public static void HorizontalLine(int length)
        {
            for (int i = 0; i < length; i++)
            {
                Console.Write('-');
            }
            Console.WriteLine();
        }
    }
}
