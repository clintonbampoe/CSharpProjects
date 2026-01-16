class Book 
{
    string Name = "Unknown";
    int Pages = 0;

    internal Book(string name, int pages)
    {
        Name = name;
        Pages = pages;
        Console.WriteLine($"Name: {Name}, Pages: {Pages}");
    }

    internal Book(int pages)
    {
        Pages = pages;
        Console.WriteLine($"Name: {Name}, Pages: {Pages}");
    }

    internal Book()
    {
        Name = "Unknown";
        Pages = 0;
        Console.WriteLine($"Name: {Name}, Pages: {Pages}");
    }
}