namespace Waypoints.Domain;

public class Category
{
    public int Id { get; private set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public string? Icon { get; set; }

    // for EF Core
    private Category() { }

    public Category(string name, string slug)
    {
        Name = name;
        Slug = slug;
    }
}