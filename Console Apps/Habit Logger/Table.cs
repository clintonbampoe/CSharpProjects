using HabitLogger;
using Microsoft.Data.Sqlite;
class Table
{

    public class Create
    {
        public static void New(SqliteConnection connection)
        {
            SqliteCommand createHabitsTable = connection.CreateCommand();
            createHabitsTable.CommandText =
            @"CREATE TABLE IF NOT EXISTS Habit (
            Name TEXT NOT NULL,
            Date TEXT NOT NULL,
            Occurance INT DEFAULT 0,
            PRIMARY KEY (Name, Date)
            );";
            createHabitsTable.ExecuteNonQuery();
        }
    }

    public class InsertRow
    {
        public static void New(SqliteConnection connection, string name, string date, int occurance)
        {
            SqliteCommand InsertHabitCmd = connection.CreateCommand();
            InsertHabitCmd.CommandText = "INSERT OR IGNORE INTO Habit (Name, Date, Occurance) VALUES ($name, $date, $occurance)";
            InsertHabitCmd.Parameters.AddWithValue("$name", name);
            InsertHabitCmd.Parameters.AddWithValue("$date", date);
            InsertHabitCmd.Parameters.AddWithValue("$occurance", occurance);
            InsertHabitCmd.ExecuteNonQuery();
        }

        public static void NameAndDateOnly(SqliteConnection connection, string name, string date)
        {
            SqliteCommand InsertHabitCmd = connection.CreateCommand();
            InsertHabitCmd.CommandText = "INSERT OR IGNORE INTO Habit (Name, Date) VALUES ($name, $date)";
            InsertHabitCmd.Parameters.AddWithValue("$name", name);
            InsertHabitCmd.Parameters.AddWithValue("$date", date);
            InsertHabitCmd.ExecuteNonQuery();
        }
    }

    public class Update
    {
        public static void Row(SqliteConnection connection, int row, int occurance)
        {
            using(SqliteCommand updateRowCmd = connection.CreateCommand())
            {
                updateRowCmd.CommandText =
                @"UPDATE Habit SET Occurance = $updateOccurance WHERE rowid = (
                    SELECT rowid FROM Habit
                    ORDER BY Name, Date
                    LIMIT 1 OFFSET $rowMinusOne
                );";
                    updateRowCmd.Parameters.AddWithValue("$updateOccurance", occurance);
                    updateRowCmd.Parameters.AddWithValue("$rowMinusOne", row-1);
                    updateRowCmd.ExecuteNonQuery();
            }
        }
    }
    public class Display
    {
        public static void All(SqliteConnection connection)
        {
            SqliteCommand selectCommand = connection.CreateCommand();
            selectCommand.CommandText = "SELECT * FROM Habit ORDER BY name, date";

            using (SqliteDataReader reader = selectCommand.ExecuteReader())
            {
                int entryNo = 0;
                while (reader.Read())
                {
                    entryNo++;
                    string name = reader.GetString(0);
                    string date = reader.GetString(1);
                    int occurance = reader.GetInt32(2);

                    Console.WriteLine("| {0, 3} | {1,-20} | {2,-20} | {3,4} |", entryNo, name, date, occurance);
                }
            }
        }
    }

    public class Delete
    {
        public static void Table(SqliteConnection connection)
        {
            SqliteCommand deleteTableCmd = connection.CreateCommand();
            deleteTableCmd.CommandText = "DROP TABLE Habit";
            deleteTableCmd.ExecuteNonQuery();
        }

        public static void Row(SqliteConnection connection, int row)
        {
            SqliteCommand deleteRowCmd = connection.CreateCommand();
            deleteRowCmd.CommandText =
            @"DELETE FROM Habit WHERE rowid = (
                SELECT rowid FROM Habit
                ORDER BY name, date
                LIMIT 1 OFFSET $rowMinusOne
            );";
            deleteRowCmd.Parameters.AddWithValue("$rowMinusOne", row - 1);
            deleteRowCmd.ExecuteNonQuery();
                
        }
    }
}