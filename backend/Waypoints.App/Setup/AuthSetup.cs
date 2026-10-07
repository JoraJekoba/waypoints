using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Waypoints.Infrastructure;

namespace Waypoints.App.Setup;

public static class AuthSetup
{
    public static void AddWaypointsAuth(this IServiceCollection services, IConfiguration config)
    {
        services.AddIdentity<AppUser, IdentityRole<int>>()
            .AddEntityFrameworkStores<WaypointDbContext>();
        
        services.AddAuthentication()
            .AddGoogle(o =>
            {
                o.ClientId = config["Authentication:Google:ClientId"]!;
                o.ClientSecret = config["Authentication:Google:ClientSecret"]!;
                o.SignInScheme = IdentityConstants.ExternalScheme;
                o.ClaimActions.MapJsonKey("picture", "picture");
            });

        services.AddAuthorization();
        
        services.ConfigureApplicationCookie(o =>
        {
            o.Cookie.SameSite = SameSiteMode.Lax;
            o.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            o.Events.OnRedirectToLogin = ctx => { ctx.Response.StatusCode = 401; return Task.CompletedTask; };
            o.Events.OnRedirectToAccessDenied = ctx => { ctx.Response.StatusCode = 403; return Task.CompletedTask; };
        });
        
        services.Configure<ForwardedHeadersOptions>(o =>
        {
            o.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            o.KnownIPNetworks.Clear();
            o.KnownProxies.Clear();
        });
    }

}