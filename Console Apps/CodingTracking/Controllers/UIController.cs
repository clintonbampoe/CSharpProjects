namespace CodingTracker.Controllers;

using CodingTracker.Models;
using CodingTracker.Views;
using System.Threading.Tasks.Dataflow;
using static CodingTracker.Models.Enums;

class UIController
{
    private UserInterface userInterface = new();

    public (MenuOption, CodingSession?) Execute()
    {
        MenuOption choice = userInterface.MainMenu();
        CodingSession session;
        int sessionId;

        switch (choice)
        {

            case MenuOption.AddSession:
                session = userInterface.AddSessionMenu();
                return (choice, session);

            case MenuOption.EditSession:
                session = userInterface.EditSession();
                return (choice, session);

            case MenuOption.DeleteSession:
                sessionId = userInterface.DeleteSession();
                session = new CodingSession(sessionId);
                return (choice, session);

            case MenuOption.ViewAllSessions:
                return (choice, null);

            default:
                return (choice, null);
        }
    }

    public void OnFetchedAllSessions(object sender, IEnumerable<CodingSession> allSessions)
    {
        userInterface.ViewAllSessions(allSessions);
    }

    public void OnDatabaseOperationCompleted(object sender, string message)
    {
        userInterface.PrintOperationSuccessfulMessage(message);
    }
}