namespace WebApp.Statics;

public static class RoleNames
{
    public const string roleAdmin = "Admin";
    public const string roleUser = "User";

    public static string[] Roles => new string[] { roleAdmin, roleUser };
}
