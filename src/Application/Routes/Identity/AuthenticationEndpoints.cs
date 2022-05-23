namespace Application.Routes.Identity;

public static class AuthenticationEndpoints
{
    public static string Register = "api/auth/register";
    public static string Login = "api/auth";
    public static string RefreshToken = $"api/auth/refresh-token";
}