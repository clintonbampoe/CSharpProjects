using Spectre.Console;

class Book : LibraryItem
{
    internal string Author { get; set; }
    internal string Category { get; set; }
    internal int Pages { get; set; }

    internal Book(int id, string name, string author, string category, string location, int pages) : base(id, name, location)
    {
        Author = author;
        Category = category;
        Pages = pages;
    }

    public override void DisplayDetails()
    {
        Panel panel = new(
            new Markup($"[bold]Book:[/] [cyan]{Name}[/] by [cyan]{Author}[/]") +
            $"\n[bold]Pages:[/] {Pages}" +
            $"\n[bold]Category:[/] {Category}" +
            $"\n[bold]Location:[/] {Location}")
        {
            Border = BoxBorder.Rounded
        };

        AnsiConsole.Write(panel);
    }
}
