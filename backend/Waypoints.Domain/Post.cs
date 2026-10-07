namespace Waypoints.Domain;

public class Post : IAuthoredContent
{
     public int Id  { get; private set; }
     public string Slug { get; private set; }
     public string Title { get; private set; }
     public string Content { get; private set; }
     //public string? Summary { get; private set; }
     public string? CoverImageKey { get; private set; }
     public ContentStatus Status { get; private set; }
     public int AuthorId { get; private set; }
     public DateTime CreatedAt { get; private set; }
     public DateTime? UpdatedAt { get; private set; }
     public DateTime? PublishedAt { get; private set; }
     
     // EF Core
     private Post() { }

     public Post(string title, string slug, string content, int authorId)
     {
          Title = title;
          Slug = slug;
          Content = content;
          AuthorId = authorId;
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