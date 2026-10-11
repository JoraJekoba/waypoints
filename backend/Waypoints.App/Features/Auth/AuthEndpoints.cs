using Microsoft.AspNetCore.Identity;
using Waypoints.App.Features.Auth.Google;
using Waypoints.Infrastructure;

namespace Waypoints.App.Features.Auth;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth");
        LoginWithGoogle.Map(group);
        GoogleCallback.Map(group);
        GetCurrentUser.Map(group);
        
        group.MapPost("/logout", async (SignInManager<AppUser> sm) =>
        {
            await sm.SignOutAsync();
            return Results.NoContent();
        });
    }
}