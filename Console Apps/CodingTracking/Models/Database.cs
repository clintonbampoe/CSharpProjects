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

    internal void Initialize()
    {
        using (SqliteConnection connection = new(_connectionString))
        {
            connection.Open();

            string command = 
                @"CREATE TABLE IF NOT EXISTS Sessions (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    startTime TEXT NOT NULL,
                    endTime TEXT NOT NULL,
                    duration TEXT NOT NULL
                );";

            connection.Execute(command);
        }
    }

    internal void AddSession(CodingSession session)
    {
        using (SqliteConnection connection = new(_connectionString))
        {
            connection.Open();
            string sql =
            @"INSERT INTO Sessions (startTime, endTime, duration) VALUES (@StartTime, @EndTime, @Duration);";

            connection.Execute(sql, session);
        }
    }

    internal int EditSession(CodingSession session)
    {
        using(SqliteConnection connection = new(_connectionString))
        {
            connection.Open();

            string sql =
            @"UPDATE Sessions 
                SET startTime = @StartTime,
                    endTime = @EndTime,
                    duration = @Duration
                WHERE id = @Id;";

            return connection.Execute(sql, session);

        }
    }

    internal int DeleteSession(CodingSession session)
    {
        using(SqliteConnection connection = new(_connectionString))
        {
            connection.Open();

            string sql =
            @"DELETE FROM Sessions
                WHERE id = @Id;";

            return connection.Execute(sql, session);
        }
    }

    public IEnumerable<CodingSession> GetAllSessions()
    {
        using (SqliteConnection connection = new(_connectionString))
        {
            connection.Open();

            return connection.Query<CodingSession>("SELECT * FROM Sessions");
        }
    }



}