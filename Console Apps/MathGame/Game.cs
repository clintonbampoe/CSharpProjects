using MathGame;

class Game
{
    public Game(GameType type)
    {
        Type = type;
    }
    public int Operand1 { get; private set; }
    public int Operand2 { get; private set; }
    public int Answer { get; private set; }
    public int Points { get; private set; }
    public Operator Operation { get; private set; }
    public GameType Type { get; init; }
    public List<string> GameHistory { get; private set; } = new();

    // MAIN
    public void Run()
    {
        SetGame();
        Question(Answer);
    }


    void SetGame()
    {
        if(Type == GameType.Random)
        {
            Random rnd = new();
            Operation = rnd.NextOperation();

            if (Operation == Operator.Divide)
            {
                // Ensures Perfect Integer Division of Operands
                HandleDivisionEdgeCase();
            }
            else
            {
                Operand1 = rnd.Next(0, 10);
                Operand2 = rnd.Next(0, 10);
            }

            Answer = Evaluate(Operation, Operand1, Operand2);
        }
        else
        {
            Random rnd = new();

            Menu.StartGame.Normal.Display();
            int input = Menu.Input(4);

            switch (input)
            {
                case 1:
                    Operation = Operator.Add;
                    break;
                case 2:
                    Operation = Operator.Subtract;
                    break;
                case 3:
                    Operation = Operator.Multiply;
                    break;
                case 4: 
                    Operation = Operator.Divide;
                    break;
            }

            if (Operation == Operator.Divide)
            {
                HandleDivisionEdgeCase(); 
            }
            else
            {
                Operand1 = rnd.Next(0, 10);
                Operand2 = rnd.Next(0, 10);
            }

            Answer = Evaluate(Operation, Operand1, Operand2);
        }
    }
    void IncrementPoints()
    {
        Points++;
    }
    void HandleDivisionEdgeCase()
    {
        bool perfectdivision;
        do
        {
            perfectdivision = true;

            Random rnd = new();
            Operand1 = rnd.Next(0, 10);
            Operand2 = rnd.Next(0, 10);
            if(Operand2 == 0)
            {
                perfectdivision = false;
            }
            else
            {
                if (Operand1 < Operand2)
                    perfectdivision = false;
                if (Operand1 % Operand2 != 0)
                    perfectdivision = false;
            }

        } while (!perfectdivision);
    }
    void Question(int actualAnswer)
    {
        char operatorSymbol = GetOperatorSymbol(Operation);
        Method.HorizontalLine(10);
        Console.WriteLine("QUESTION");

        Console.WriteLine($"Evaluate: {Operand1} {operatorSymbol} {Operand2}");

        // Input Validation
        int userInput;
        bool validInput;
        do
        {
            Console.Write(">>>");
            validInput = int.TryParse(Method.TakeInput(), out userInput);

            if (validInput)
                break;
            else
            {
                Message.RedText("Invalid Input!!!");
                Message.RedText("Try Again");
            }
        } while (!validInput);

        if(userInput == actualAnswer)
        {
            Message.GreenText("Correct Answer!!!");
            Message.GreenText("+1 point(s)");
            IncrementPoints();
        }
        else
        {
            Message.RedText("Wrong Answer!!!");
        }

        LogToGameHistory(Operation, Operand1, Operand2, userInput);

        Console.WriteLine();
    }
    char GetOperatorSymbol(Operator op)
    {
        if (op == Operator.Add)
            return '+';
        else if (op == Operator.Subtract)
            return '-';
        else if (op == Operator.Multiply)
            return '*';
        else
            return '/';
    }
    int Evaluate(Operator operation, int num1, int num2)
    {
        switch (operation)
        {
            case Operator.Add:
                return num1 + num2;
            case Operator.Subtract:
                return num1 - num2;
            case Operator.Multiply:
                return num1 * num2;
            case Operator.Divide:
                return num1 / num2;

            default: return default;
        }
    }
    void LogToGameHistory(Operator op, int num1, int num2, int answer)
    {
        GameHistory.Add($"Evaluate: {num1} {GetOperatorSymbol(op)} {num1}");
        GameHistory.Add($">>> {answer}");
    }
    public static void ShowHistory(List<string> gameHistory)
    {
        foreach(string str in gameHistory)
        {
            Console.WriteLine(str);
        }
    }
    public void DisplayPoints()
    {
        Console.Clear();
        Console.WriteLine("GAME OVER");
        Method.HorizontalLine(10);
        Console.WriteLine();

        if (Points > 0)
            Message.GreenText($"You had {Points} points!");
        else
            Message.RedText($"You had {Points} points!");

        Console.WriteLine();
        Console.WriteLine("Press any key to proceed...");
        Console.ReadKey();
    }

}

namespace MathGame
{
    public enum Operator { Add, Subtract, Multiply, Divide }
    public enum GameType { Normal, Random }
}
