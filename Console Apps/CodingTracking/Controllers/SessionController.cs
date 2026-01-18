using Microsoft.Data.Sqlite;
using Dapper;
using CodingTracker.Models;
using static CodingTracker.Models.Enums;

namespace CodingTracker.Controllers;

class SessionController
{
    private readonly Database _database;
    public event EventHandler<string>? DatabaseOperationCompleted;
    public event EventHandler<IEnumerable<CodingSession>> FetchedAllSessions;
    public SessionController(Database database, EventHandler<IEnumerable<CodingSession>> fetchedAllSessionsHandler)
    {
        _database = database;
        FetchedAllSessions = fetchedAllSessionsHandler;
    }

    public void Execute(MenuOption choice, CodingSession session)
    {
        switch (choice)
        {
            case MenuOption.AddSession:
                AddSession(session);
                break;

            case MenuOption.EditSession:
                EditSession(session);
                break;

            case MenuOption.DeleteSession:
                DeleteSession(session);
                break;
            case MenuOption.ViewAllSessions:
                GetAllSessions();
                break;
        }
    }


    private void AddSession(CodingSession session)
    {
        _database.AddSession(session);
        OnOperationCompleted("Inserted");
    }

    private void EditSession(CodingSession session)
    {
        _database.EditSession(session);
        OnOperationCompleted("Edited");
    }

    private void DeleteSession(CodingSession session)
    {
        _database.EditSession(session);
        OnOperationCompleted("Deleted");
    }

    private void GetAllSessions()
    {
        IEnumerable<CodingSession> allSessions = _database.GetAllSessions();
        OnFetchedAllSessions(allSessions);
    }

    // event broadcasters
    private void OnOperationCompleted(string message)
    {
        DatabaseOperationCompleted?.Invoke(this, message);
    }
    private void OnFetchedAllSessions(IEnumerable<CodingSession> allSessions)
    {
        FetchedAllSessions?.Invoke(this, allSessions);
    }
}
