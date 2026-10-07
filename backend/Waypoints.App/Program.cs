using Microsoft.EntityFrameworkCore;
using Waypoints.App.Features.Auth;
using Waypoints.App.Setup;
using Waypoints.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WaypointDbContext>(o =>
    o.UseSqlite(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddWaypointsAuth(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseForwardedHeaders();
app.UseAuthentication();
app.UseAuthorization();

app.MapAuthEndpoints();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<WaypointDbContext>();
    db.Database.Migrate();
}

app.MapGet("/", () => "Hello World!");

app.Run();