using Microsoft.Data.Sqlite;
using Dapper;
namespace CodingTracker.Models;

class Database
{
    private string _connectionString;

    public Database(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void Initialize()
    {
        using (SqliteConnection connection = new())
        {
            connection.Open();

            SqliteCommand createTable = connection.CreateCommand();
            createTable.CommandText = 
                @"CREATE TABLE IF NOT EXISTS Sessions (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    StartTime TEXT NOT NULL,
                    Duration INTEGER NOT NULL,
                    Topic TEXT NOT NULL
                );";

            connection.Execute(createTable.CommandText);
        }
    }
}