namespace Application.Routes.Projects;

public static class TicketEndpoints
{
    public static string Get(int id) => $"api/ticket/{id}";
    public static string GetByProject(int projectId) => $"api/ticket/project/{projectId}";
    public static string Add = "api/ticket";
    public static string Edit = "api/ticket";
    public static string Delete(int ticketId) => $"api/ticket/{ticketId}";

}