using System.Net.Http.Json;
using Application.Projects.Commands;
using Application.Projects.Responses;
using Application.Routes.Projects;

namespace WebUI.Managers.Projects;

public interface ITicketManager
{
    Task<List<TicketResponse>> GetByProject(int projectId);
    Task SaveAsync(AddEditTicketCommand command);
    Task<TicketResponse> GetByIdAsync(int ticketId);
    Task DeleteAsync(int ticketId);
}

public class TicketManager : ITicketManager
{
    private readonly HttpClient _httpClient;
    public TicketManager(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<List<TicketResponse>> GetByProject(int projectId)
    {
        return await _httpClient.GetFromJsonAsync<List<TicketResponse>>(TicketEndpoints.GetByProject(projectId));
    }
    public async Task SaveAsync(AddEditTicketCommand command)
    {
        var result = await _httpClient.PostAsJsonAsync(TicketEndpoints.Edit, command);
    }
    public async Task<TicketResponse> GetByIdAsync(int ticketId)
    {
        return await _httpClient.GetFromJsonAsync<TicketResponse>(ProjectEndpoints.GetById(ticketId));
    }
    public async Task DeleteAsync(int ticketId)
    {
        var result = await _httpClient.DeleteAsync(TicketEndpoints.Delete(ticketId));
    }
}