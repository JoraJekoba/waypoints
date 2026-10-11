using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Waypoints.Infrastructure;

namespace Waypoints.App.Features.Auth;

public static class GetCurrentUser
{
    public record Response(int Id, string? DisplayName, string? AvatarUrl, IList<string> Roles);

    public static void Map(RouteGroupBuilder group) =>
        group.MapGet("/me", Handle).RequireAuthorization();

    private static async Task<IResult> Handle(ClaimsPrincipal principal, UserManager<AppUser> userManager)
    {
        var user = await userManager.GetUserAsync(principal);
        if (user is null) return Results.Unauthorized();

        var roles = await userManager.GetRolesAsync(user);
        return Results.Ok(new Response(user.Id, user.DisplayName, user.AvatarUrl, roles));
    }
}