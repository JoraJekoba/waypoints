namespace Waypoints.App.Setup;

public static class Roles
{
    public const string Author = "Author";
    public const string Editor = "Editor";
    public const string Admin = "Admin";

    public static readonly string[] All = [Author, Editor, Admin];
}