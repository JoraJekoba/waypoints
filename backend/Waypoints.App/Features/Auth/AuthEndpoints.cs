using Waypoints.App.Features.Auth.Google;

namespace Waypoints.App.Features.Auth;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth");
        LoginWithGoogle.Map(group);
        GoogleCallback.Map(group);
        GetCurrentUser.Map(group);
        //Logout.Map(group);
    }
}