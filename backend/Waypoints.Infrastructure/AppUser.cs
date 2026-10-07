using Microsoft.AspNetCore.Identity;

namespace Waypoints.Infrastructure;

public class AppUser : IdentityUser<int>
{
    public string? DisplayName { get; set; }
    public string? AvatarUrl { get; set; }
}