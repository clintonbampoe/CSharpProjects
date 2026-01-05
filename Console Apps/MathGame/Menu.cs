using MathGame;

class Menu
{
    public class Main
    {
        public static void Display()
        {
            Console.WriteLine("MAIN MENU");
            Method.HorizontalLine(20);
            Console.WriteLine("1. New Game");
            Console.WriteLine("2. Game History");
            Console.WriteLine("3. Quit");
            Console.WriteLine();
        }
    }
    public class StartGame
    {
        public class Normal
        {
            public static void Display()
            {
                Console.WriteLine("SELECT OPERATOR");
                Method.HorizontalLine(20);
                Console.WriteLine("1. Add (+)");
                Console.WriteLine("2. Subtract (-)");
                Console.WriteLine("3. Multiply (*)");
                Console.WriteLine("4. Divide (/)");
                Console.WriteLine();
            }
        }
        public static void Display()
        {
            Console.WriteLine("SELECT GAME MODE");
            Method.HorizontalLine(20);
            Console.WriteLine("1. Normal");
            Console.WriteLine("2. Random");
            Console.WriteLine("3. Back");
            Console.WriteLine();
        }
    }


    // Methods
    public static int Input(int numOfChoices)
    {
        do
        {
            Console.WriteLine("Enter: ");
            Console.Write(">>>");
            int input;
            bool isValid = int.TryParse(Method.TakeInput(), out input);
            
            if (isValid || (input > 0 && input <= numOfChoices))
            {
                return input;
            }
            else
            {
                Message.RedText("Invalid Input");
            }

        } while (true);
    }
}