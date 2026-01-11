using HabitLogger;
using Microsoft.Data.Sqlite;
class Table
{

    public class Create
    {
        public static void NewTable(SqliteConnection connection)
        {
            using(SqliteCommand createHabitsTable = connection.CreateCommand())
            {
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
    }

    public class InsertRow
    {
        public static void New(SqliteConnection connection, string name, string date, int occurance)
        {
            using (SqliteCommand InsertHabitCmd = connection.CreateCommand())
            {
                InsertHabitCmd.CommandText = "INSERT OR IGNORE INTO Habit (Name, Date, Occurance) VALUES ($name, $date, $occurance)";
                InsertHabitCmd.Parameters.AddWithValue("$name", name);
                InsertHabitCmd.Parameters.AddWithValue("$date", date);
                InsertHabitCmd.Parameters.AddWithValue("$occurance", occurance);
                InsertHabitCmd.ExecuteNonQuery();
            }
        }
        
        public static void NameAndDateOnly(SqliteConnection connection, string name, string date)
        {
            using(SqliteCommand InsertHabitCmd = connection.CreateCommand())
            {
                InsertHabitCmd.CommandText = "INSERT OR IGNORE INTO Habit (Name, Date) VALUES ($name, $date)";
                InsertHabitCmd.Parameters.AddWithValue("$name", name);
                InsertHabitCmd.Parameters.AddWithValue("$date", date);
                InsertHabitCmd.ExecuteNonQuery();
            }
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
                int updated = updateRowCmd.ExecuteNonQuery();

                if(updated == 0)
                {
                    Method.Print.RedText("No row updated - offset out of range");
                }
            }
        }
    }


    public class Display
    {
        public static void All(SqliteConnection connection)
        {
            using (SqliteCommand selectCommand = connection.CreateCommand())
            {
                selectCommand.CommandText = "SELECT * FROM Habit ORDER BY name, date";

                using (SqliteDataReader reader = selectCommand.ExecuteReader())
                {
                    Method.Print.GreenText("HABITS");

                    Method.Formatting.HorizontalLine(67);
                    Console.WriteLine("| {0,5} | {1,-20} | {2,-20} | {3,9} |", "ENTRY", "NAME", "DATE", "OCCURANCE");
                    Method.Formatting.HorizontalLine(67);

                    int entryNo = 0;
                    while (reader.Read())
                    {
                        entryNo++;
                        string name = reader.GetString(0);
                        string date = reader.GetString(1);
                        int occurance = reader.GetInt32(2);

                        Console.WriteLine("| {0, 5} | {1,-20} | {2,-20} | {3,9} |", entryNo, name, date, occurance);
                        Method.Formatting.HorizontalLine(67);
                    }
                }
            }
           
        }
    }


    public class Delete
    {
        public static void Table(SqliteConnection connection)
        {
            using(SqliteCommand deleteTableCmd = connection.CreateCommand())
            {
                deleteTableCmd.CommandText = "DROP TABLE Habit";
                deleteTableCmd.ExecuteNonQuery();
            }
        }

        public static void Row(SqliteConnection connection, int row)
        {
            using(SqliteCommand deleteRowCmd = connection.CreateCommand())
            {
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
}