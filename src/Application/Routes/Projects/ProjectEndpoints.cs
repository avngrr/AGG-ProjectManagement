namespace Application.Routes.Projects;

public static class ProjectEndpoints
{
    public static string GetAll = "api/project";
    public static string GetById(int projectId) => $"api/project/{projectId}";
    public static string Add = "api/project";
    public static string Edit = "api/project";
    public static string Delete(int projectId) => $"api/project/{projectId}";
    public static string Complete(int projectId) => $"api/project/complete/{projectId}";
    public static string ResetComplete(int projectId) => $"api/project/reset/{projectId}";
}