using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Waypoints.Infrastructure;

namespace Waypoints.App.Features.Auth.Google;

public static class LoginWithGoogle
{
    public static void Map(RouteGroupBuilder group) => group.MapGet("/login/google", Handle);

    private static IResult Handle(string? returnUrl, SignInManager<AppUser> signInManager)
    {
        var callbackUrl = $"/api/auth/callback?returnUrl={Uri.EscapeDataString(returnUrl ?? "/")}";
        var props = signInManager.ConfigureExternalAuthenticationProperties(
            GoogleDefaults.AuthenticationScheme, callbackUrl);

        return Results.Challenge(props, [GoogleDefaults.AuthenticationScheme]);
    }
}