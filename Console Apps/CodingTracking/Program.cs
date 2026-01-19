using Microsoft.Extensions.Configuration;
using SQLitePCL;
using CodingTracker.Models;
using CodingTracker.Controllers;

namespace CodingTracker;

class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Batteries.Init();

        var config = new ConfigurationBuilder()
             .SetBasePath(AppContext.BaseDirectory)
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
             .Build();

        string connectionString = config.GetConnectionString("DefaultConnection");

        Database database = new(connectionString);
        var userInterface = new UIController();
        var sessionController = new SessionController(database);
        database.Initialize();

        sessionController.FetchedAllSessions += userInterface.OnFetchedAllSessions;
        sessionController.DatabaseOperationCompleted += userInterface.OnDatabaseOperationCompleted;
        sessionController.DatabaseOperationFailed += userInterface.OnDatabaseOperationFailed;

        while (true)
        {
            var (choice, session) = userInterface.Execute();
            sessionController.Execute(choice, session);
        }

    }
}