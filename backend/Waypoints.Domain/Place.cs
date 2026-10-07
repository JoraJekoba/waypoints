namespace Waypoints.Domain;

public class Place : IAuthoredContent
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Slug { get; private set; }
    public string Description { get; private set; }
    public int CategoryId { get; private set; }
    public Category Category { get; private set; } = null!;
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }
    public string? Address  { get; private set; }
    public int AuthorId { get; private set; }
    public ContentStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? PublishedAt { get; private set; }
    
    private Place() { }

    public Place(string name, string slug, string description, int categoryId, int authorId, double latitude, double longitude)
    {
        Name = name;
        Slug = slug;
        Description = description;
        CategoryId = categoryId;
        AuthorId = authorId;
        Latitude = latitude;
        Longitude = longitude;
        Status = ContentStatus.Draft;
        CreatedAt = DateTime.UtcNow;
    }
     
    public void Publish()
    {
        Status = ContentStatus.Published;
        PublishedAt ??= DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Hide()
    {
        Status = ContentStatus.Hidden;
        UpdatedAt = DateTime.UtcNow;
    }
}