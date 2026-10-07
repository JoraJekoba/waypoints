
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Waypoints.Domain;
using Waypoints.Infrastructure.Converters;

namespace Waypoints.Infrastructure;

public class WaypointDbContext(DbContextOptions<WaypointDbContext> options)
    : IdentityDbContext<AppUser, IdentityRole<int>, int>(options)
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Place> Places { get; set; }
    //public DbSet<Photo> Photos { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<Category>(e => e.HasIndex(c => c.Slug).IsUnique());
        
        builder.Entity<Place>(e =>
        {
            e.Property(p => p.Status).HasConversion<string>();
            e.HasIndex(p => p.Slug).IsUnique();
            e.HasOne<AppUser>().WithMany().HasForeignKey(p => p.AuthorId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(p => p.Category).WithMany().HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.Restrict);
        });
        
        builder.Entity<Post>(e =>
        {
            e.Property(p => p.Status).HasConversion<string>();
            e.HasIndex(p => p.Slug).IsUnique();
            e.HasOne<AppUser>().WithMany().HasForeignKey(p => p.AuthorId).OnDelete(DeleteBehavior.Restrict);
        });
        
        builder.Entity<Comment>(e =>
        {
            e.HasOne<Post>().WithMany().HasForeignKey(c => c.PostId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne<Place>().WithMany().HasForeignKey(c => c.PlaceId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne<Comment>().WithMany().HasForeignKey(c => c.ParentId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne<AppUser>().WithMany().HasForeignKey(c => c.AuthorId).OnDelete(DeleteBehavior.Restrict);
            e.ToTable(t => t.HasCheckConstraint("CK_Comment_SingleTarget",
                "(\"PostId\" IS NULL) <> (\"PlaceId\" IS NULL)"));
        });
    }
    
    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        builder.Properties<DateTime>().HaveConversion<UtcDateTimeConverter>();
        builder.Properties<DateTime?>().HaveConversion<UtcDateTimeConverter>();
    }
}