using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Waypoints.Infrastructure;

public static class GoogleCallback
{
    public static void Map(RouteGroupBuilder group) => group.MapGet("/callback", Handle);

    private static async Task<IResult> Handle(
        string? returnUrl,
        HttpContext http,
        SignInManager<AppUser> signInManager,
        UserManager<AppUser> userManager)
    {
        // 1. Что сказал Google (читается из временной External cookie)
        var info = await signInManager.GetExternalLoginInfoAsync();
        if (info is null)
            return Results.Redirect("/?error=login_failed");

        // 2. Пытаемся войти по связке (Google, sub) из AspNetUserLogins
        var result = await signInManager.ExternalLoginSignInAsync(
            info.LoginProvider, info.ProviderKey, isPersistent: true, bypassTwoFactor: true);

        if (result.IsLockedOut)
            return Results.Redirect("/?error=blocked");

        // 3. Связки нет — первый вход, регистрируем
        if (!result.Succeeded)
        {
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            if (email is null)
                return Results.Redirect("/?error=no_email");

            var user = new AppUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true,   // Google уже подтвердил email
                DisplayName = info.Principal.FindFirstValue(ClaimTypes.Name),
                AvatarUrl = info.Principal.FindFirstValue("picture")
            };

            var created = await userManager.CreateAsync(user);
            if (!created.Succeeded)
                return Results.Redirect("/?error=registration_failed");

            await userManager.AddLoginAsync(user, info);
            await signInManager.SignInAsync(user, isPersistent: true);
        }

        // 4. Временная cookie больше не нужна
        await http.SignOutAsync(IdentityConstants.ExternalScheme);

        // 5. Защита от open redirect
        var target = returnUrl is not null && returnUrl.StartsWith('/') && !returnUrl.StartsWith("//")
            ? returnUrl
            : "/";
        return Results.Redirect(target);
    }
}