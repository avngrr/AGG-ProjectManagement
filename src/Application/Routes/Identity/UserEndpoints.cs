namespace Application.Routes.Identity;

public static class UserEndpoints
{
    public static string GetAll = "api/user";
    public static string Get(string userId) => $"api/user/{userId}";
    public static string GetRoles(string userId) => $"api/user/roles/{userId}";
    public static string UpdateRoles = "api/user/roles/update";
    public static string SetActive = "api/user/active";
}