using Microsoft.Extensions.Configuration;
using CodingTracker.Views;
using CodingTracker.Models;
using CodingTracker.Controllers;

namespace CodingTracker;

class Program
{
    public static void Main(string[] args)
    {
        var config = new ConfigurationBuilder()
             .SetBasePath(AppContext.BaseDirectory)
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
             .Build();

        string connectionString = config.GetConnectionString("DefaultConnection");

        Database database = new(connectionString);
        SessionController sessionController = new(database);
        UIController uiController = new();


        var(choice, session) = uiController.Execute();
        sessionController.Execute(choice, session);

    }
}