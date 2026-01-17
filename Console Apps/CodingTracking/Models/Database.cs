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

    internal void EditSession(CodingSession session, int id)
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

            connection.Execute(sql, session);
        }
    }

    internal void DeleteSession(int id)
    {
        using(SqliteConnection connection = new(_connectionString))
        {
            connection.Open();

            string sql =
            @"DELETE Sessions
                WHERE id = @Id;";

            connection.Execute(sql, new {Id = id});
        }
    }
    public IEnumerable<CodingSession> GetAllSessions()
    {
        using (SqliteConnection connection = new(_connectionString))
        {
            connection.Open();

            return connection.Query<CodingSession>("SELECT * FROM Session");
        }
    }



}