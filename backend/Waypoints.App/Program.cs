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

WebApplication app = builder.Build();

await app.InitializeDatabaseAsync();

app.UseForwardedHeaders();
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapAuthEndpoints();
app.MapGet("/", () => "Hello World!");

app.Run();