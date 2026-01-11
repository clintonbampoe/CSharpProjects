# HABIT LOGGER
## OVERVIEW

Habit Logger is a C# console application built as a hobby project.

## FEATURES
- Add new habits with details such as name, date, unit and quantity.
- Log habits by date and track their occurences.
- View all logged habits stored in the database.
- Update or delete existing habit entries.
- Automatically creates a SQLite database and table if they do not exist


## REQUIREMENTS
- .NET SDK (6.0 or later recommended)
- SQLite Library(`Microsoft.Data.Sqlite`)
- Any code editor (e.g. VS Code, VS IDE, etc)

## INSTALLATION
- Clone or download project files
- Open solution in prefered C# editor
- Ensure `Microsoft.Data.Sqlite` package is installed 

## USAGE
- Run the app and see 😫.

## DATABASE SCHEMA
The application creates a table called `Habit` with the following structure:
```SQL
CREATE TABLE IF NOT EXISTS Habit (
    Name TEXT NOT NULL,
    Date TEXT NOT NULL,
    Unit TEXT,
    Quantity INT DEFAULT 0,
    PRIMARY KEY (Name, Date)
);
```

## Learning Goals
This project demonstrates:
- How to connect a C# application to a SQLite database.
- How to execute SQL commands from C#.
- How to design and implement CRUD functionality in a console application.
- How to structure code for clarity and maintainability.

