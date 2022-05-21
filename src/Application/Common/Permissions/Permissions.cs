namespace Application.Common.Permissions;

public static class Permissions
{
    public static List<string> GeneratePermissionsForModule(string module)
    {
        return new List<string>()
        {
            $"Permissions.{module}.Create",
            $"Permissions.{module}.View",
            $"Permissions.{module}.Edit",
            $"Permissions.{module}.Delete",
        };
    }
    public static class Users
    {
        public const string View = "Permissions.Users.View";
        public const string Edit = "Permissions.Users.Edit";
        public const string Create = "Permissions.Users.Create";
        public const string Delete = "Permissions.Users.Delete";
    }
    public static class Roles
    {
        public const string View = "Permissions.Roles.View";
        public const string Edit = "Permissions.Roles.Edit";
        public const string Create = "Permissions.Roles.Create";
        public const string Delete = "Permissions.Roles.Delete";
    }
    public static class Projects
    {
        public const string View = "Permissions.Projects.View";
        public const string Edit = "Permissions.Projects.Edit";
        public const string Create = "Permissions.Projects.Create";
        public const string Delete = "Permissions.Projects.Delete";
    }
}