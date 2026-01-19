# CodingTracker

Simple console application to record and manage coding sessions. Built with .NET 9 (C# 13). The program uses SQLite (via `SQLitePCL`) for storage and a small event-driven controller layer to separate UI and data operations.

## Key files and components
- `Program.cs` — application entry point. Initializes SQLite, reads `appsettings.json`, wires controllers and starts the main input loop.
- `appsettings.json` — contains the `DefaultConnection` connection string used by `Database`.
- `Database` — encapsulates SQLite access and schema initialization (`Database.Initialize()`).
- `SessionController` — business/controller layer that executes operations and raises events:
  - `FetchedAllSessions`
  - `DatabaseOperationCompleted`
  - `DatabaseOperationFailed`
- `UIController` — console UI that displays menus, reads user input, and handles events from `SessionController`.
- `CodingTracker.Models`, `CodingTracker.Controllers` — namespaces that hold models and controller code.

## Requirements
- .NET 9 SDK
- Visual Studio 2022 or later (or the `dotnet` CLI)
- Platform-appropriate SQLite native library (handled via `SQLitePCL` and `Batteries.Init()`)

## Quick start — CLI
1. Clone the repository:
   - `git clone <repo-url>`
2. From the solution/root folder:
   - `dotnet restore`
   - `dotnet build`
   - `dotnet run --project <path-to-console-project>` (or run via Visual Studio)

## Configuration
- Edit `appsettings.json` to set your database connection:
  - Provide a valid SQLite connection string under `ConnectionStrings:DefaultConnection`.
- The app sets `Console.OutputEncoding = System.Text.Encoding.UTF8` and calls `Batteries.Init()` to prepare `SQLitePCL`.

Example `appsettings.json` fragment:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=codingtracker.db"
  }
}
```

## How it works (high level)
1. `Program.cs` reads `appsettings.json` and obtains the `DefaultConnection`.
2. `Database` is instantiated and `Initialize()` ensures tables/schema exist.
3. `UIController` and `SessionController` are created. The UI subscribes to controller events to display results or errors.
4. The app enters a loop: the UI returns a `(choice, session)` tuple and `SessionController.Execute(choice, session)` performs the requested operation.

This design keeps UI, business logic, and data access separated and event-driven for clear responsibilities.

## Troubleshooting
- If the app fails to open the DB: confirm `DefaultConnection` in `appsettings.json` and file permissions.
- If `SQLitePCL.raw` errors occur, ensure native SQLite binaries are available for your OS/architecture or let NuGet restore the required packages and rebuild.
- If configuration is not picked up, confirm the working directory contains `appsettings.json` (the app uses `AppContext.BaseDirectory`).

## Contributing and style
- Follow the repository's `.editorconfig` and `CONTRIBUTING.md` for formatting, naming, and contribution guidelines.
- Keep changes small, document intent in PRs, and run the solution locally before submitting.

## Notes
- The project targets modern .NET and aims for clarity and small surface area. Use dependency injection or tests as future improvements if the codebase grows.