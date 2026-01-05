namespace MathGame;
internal class Program
{
    public static void Main(string[] args)
    {
        // Init Game
        List<List<string>> History = new();

        while(true) {
            Console.Clear();
            Menu.Main.Display();
            int input = Menu.Input(3);

            switch (input)
            {
                case 1:
                    StartGame();
                    break;
                case 2:
                    Program.ShowHistory(History);
                    PressKeyToProceed();
                    break;
                case 3:
                default:
                    QuitGameConfirmationMessage();
                    return;
            }

        }



        void StartGame()
        {
            Console.Clear();
            Menu.StartGame.Display();
            int input = Menu.Input(3);

            switch (input)
            {
                case 1:
                    CreateNewGame(GameType.Normal);
                    break;
                case 2:
                    CreateNewGame(GameType.Random);
                    break;

                case 3:
                default:
                    return;
            }
        }

        void CreateNewGame(GameType type)
        {
            DateTime now = DateTime.Now;
            if (type == GameType.Normal)
            {
                Game game = new(GameType.Normal);
                for (int i = 0; i < 5; i++)
                {
                    game.Run();
                }
                TimeElasped(now);
                PressKeyToProceed();

                game.DisplayPoints();
                History.Add(game.GameHistory);
            }
            else
            {
                Game game = new(GameType.Random);
                for (int i = 0; i < 5; i++)
                {
                    game.Run();
                }
                TimeElasped(now);
                PressKeyToProceed();

                game.DisplayPoints();
                History.Add(game.GameHistory);
            }
        }

        static void QuitGameConfirmationMessage()
        {
            Message.RedText("Are you sure");
            PressKeyToProceed();
        }
        static void PressKeyToProceed()
        {
            Console.WriteLine("Press any key to proceed...");
            Console.ReadKey();
        }
        static void TimeElasped(DateTime startTime)
        {
            TimeSpan timeElapsed = DateTime.Now - startTime;
            Console.WriteLine($"You used {timeElapsed.Hours} hours {timeElapsed.Minutes} minutes and {timeElapsed.Seconds} seconds");
        }
    }


    static void ShowHistory(List<List<string>> History)
    {
        foreach(List<string> gameHistory in History)
        {
            Console.WriteLine("NEW GAME");
            Method.HorizontalLine(20);
            Game.ShowHistory(gameHistory);
            Console.WriteLine();
        }


    }
}