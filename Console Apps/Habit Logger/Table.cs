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
                Unit TEXT,
                Quantity INT DEFAULT 0,
                PRIMARY KEY (Name, Date)
                );";
                createHabitsTable.ExecuteNonQuery();
            }
        }
    }

    public class InsertRow
    {
        public static void New(SqliteConnection connection, string name, string date, string unit, int quantity)
        {
            using (SqliteCommand InsertHabitCmd = connection.CreateCommand())
            {
                InsertHabitCmd.CommandText = "INSERT INTO Habit (Name, Date, Unit, Quantity) VALUES ($name, $date, $unit, $quantity)";
                InsertHabitCmd.Parameters.AddWithValue("$name", name);
                InsertHabitCmd.Parameters.AddWithValue("$date", date);
                InsertHabitCmd.Parameters.AddWithValue("$unit", unit);
                InsertHabitCmd.Parameters.AddWithValue("$quantity", quantity);
                InsertHabitCmd.ExecuteNonQuery();
            }
        }
        
        public static void NameDateAndUnitOnly(SqliteConnection connection, string name, string date, string unit)
        {
            using(SqliteCommand InsertHabitCmd = connection.CreateCommand())
            {
                InsertHabitCmd.CommandText = "INSERT INTO Habit (Name, Date, Unit) VALUES ($name, $date, $unit)";
                InsertHabitCmd.Parameters.AddWithValue("$name", name);
                InsertHabitCmd.Parameters.AddWithValue("$date", date);
                InsertHabitCmd.Parameters.AddWithValue("$unit", unit);
                InsertHabitCmd.ExecuteNonQuery();
            }
        }
    }

    public class UpdateRow
    {
        public static void Quantity(SqliteConnection connection, int row, int quantity)
        {
            using(SqliteCommand updateQuantityField = connection.CreateCommand())
            {
                updateQuantityField.CommandText =
                @"UPDATE Habit SET Quantity = $updateQuantity WHERE rowid = (
                    SELECT rowid FROM Habit
                    ORDER BY Name, Date
                    LIMIT 1 OFFSET $rowMinusOne
                );";
                updateQuantityField.Parameters.AddWithValue("$updateQuantity", quantity);
                updateQuantityField.Parameters.AddWithValue("$rowMinusOne", row-1);
                int updated = updateQuantityField.ExecuteNonQuery();

                if(updated == 0)
                {
                    Method.Print.RedText("No row updated - offset out of range");
                }
            }
        }

        public static void Unit(SqliteConnection connection, int row, string unit)
        {
            using(SqliteCommand updateUnitField = connection.CreateCommand())
            {
                updateUnitField.CommandText =
                @"UPDATE Habit SET Quantity = $updateUnit WHERE rowid = (
                    SELECT rowid FROM Habit
                    ORDER BY Name, Date
                    LIMIT 1 OFFSET $rowMinusOne
                );";
                updateUnitField.Parameters.AddWithValue("$updateUnit", unit);
                updateUnitField.Parameters.AddWithValue("$rowMinusOne", row - 1);
                int updated = updateUnitField.ExecuteNonQuery();

                if (updated == 0)
                {
                    Method.Print.RedText("No row updated - offset out of range");
                }
            }
        }
    }


    public class Display
    {
        public static void AllRows(SqliteConnection connection)
        {
            using (SqliteCommand selectCommand = connection.CreateCommand())
            {
                selectCommand.CommandText = "SELECT * FROM Habit ORDER BY name, date";

                using (SqliteDataReader reader = selectCommand.ExecuteReader())
                {
                    Console.WriteLine("HABIT TABLE");

                    Method.Formatting.HorizontalLine(89);
                    Console.WriteLine("| {0,5} | {1,-20} | {2,-20} | {3,-20} | {4,8} |", "ENTRY", "NAME", "DATE", "UNIT", "QUANTITY");
                    Method.Formatting.HorizontalLine(89);

                    int entryNo = 0;
                    while (reader.Read())
                    {
                        entryNo++;
                        string name = reader.GetString(0);
                        string date = reader.GetString(1);
                        string unit = reader.GetString(2);
                        int quantity = reader.GetInt32(3);

                        Console.WriteLine("| {0, 5} | {1,-20} | {2,-20} | {3,-20} | {4,8} |", entryNo, name, date, unit, quantity);
                        Method.Formatting.HorizontalLine(89);
                    }
                }
            }
           
        }

        public static void TableInfo(SqliteConnection connection)
        {
            using (SqliteCommand tableInfoCmd = connection.CreateCommand())
            {
                tableInfoCmd.CommandText = "PRAGMA table_info(Habit);";
                using (SqliteDataReader reader = tableInfoCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["name"]} - {reader["type"]}");
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