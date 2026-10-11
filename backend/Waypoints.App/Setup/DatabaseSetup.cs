using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Waypoints.Infrastructure;

namespace Waypoints.App.Setup;

public static class DatabaseSetup
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        
        var db = scope.ServiceProvider.GetRequiredService<WaypointDbContext>();
        await db.Database.MigrateAsync();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
        foreach (var role in Roles.All)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole<int>(role));
        }
    }
}