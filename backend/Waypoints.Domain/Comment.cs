namespace Waypoints.Domain;

public class Comment
{
    public int Id { get; private set; }
    public int? PostId { get; private set; }
    public int? PlaceId { get; private set; }
    public int? ParentId { get; private set; }
    public string Content { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public bool IsDeleted { get; private set; }
    public int AuthorId { get; private set; }

    // for EF Core
    private Comment()
    {
        
    }

    public void Edit(string content)
    {
        Content = content;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Delete()
    {
        IsDeleted = true;
    }
    
    public static Comment ForPost(int postId, int authorId, string content) => new()
        { PostId = postId, AuthorId = authorId, Content = content, CreatedAt = DateTime.UtcNow };
    
    public static Comment ForPlace(int placeId, int authorId, string content) => new()
        { PlaceId = placeId, AuthorId = authorId, Content = content, CreatedAt = DateTime.UtcNow };
    
    public Comment Reply(int authorId, string content) => new()
    {
        PostId = PostId,
        PlaceId = PlaceId,
        ParentId = Id,
        AuthorId = authorId,
        Content = content,
        CreatedAt = DateTime.UtcNow
    };
}