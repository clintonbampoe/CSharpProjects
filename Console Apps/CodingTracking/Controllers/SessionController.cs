using Microsoft.Data.Sqlite;
using Dapper;
using CodingTracker.Models;
using static CodingTracker.Models.Enums;

namespace CodingTracker.Controllers;

class SessionController
{
    private readonly Database _database;
    public event EventHandler<string>? DatabaseOperationCompletedSuccessfully;
    public SessionController(Database database)
    {
        _database = database;
    }

    public void Execute(MenuOption choice, CodingSession session)
    {
        switch (choice)
        {
            case MenuOption.AddSession:
                _database.AddSession(session);
                break;
            case MenuOption.EditSession:
                _database.EditSession(session);
                break;
            case MenuOption.DeleteSession:
                _database.DeleteSession(session);
                break;
            case MenuOption.ViewAllSessions:
                _database.GetAllSessions();
                break;
            default:
                break;
        }
    }
    void AddSession(CodingSession session)
    {
        _database.AddSession(session);
    }

    void EditSession()
    {

    }

    void DeleteSession()
    {

    }

    void GetAllSessions()
    {

    }

    void OnDatabaseOperationCompleted(string message)
    {
        DatabaseOperationCompletedSuccessfully?.Invoke(this, message);
    }
}
