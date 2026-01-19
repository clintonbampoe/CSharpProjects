namespace CodingTracker.Controllers;

using CodingTracker.Models;
using CodingTracker.Views;
using static CodingTracker.Services.Validation;
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
                do
                {
                    session = userInterface.AddSession();

                    if (!IsValidSession(session))
                        userInterface.InvalidInput("[blue]End Time[/] cannot be earlier than [blue]Start Time[/]");

                } while (!IsValidSession(session));
                return (choice, session);

            case MenuOption.EditSession:
                do
                {
                    session = userInterface.EditSession();

                    if(!IsValidSession(session))
                        userInterface.InvalidInput("[blue]End Time[/] cannot be earlier than [blue]Start Time[/]");

                } while (!IsValidSession(session));
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