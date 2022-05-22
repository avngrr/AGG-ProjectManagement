using System.Net.Http.Json;
using Application.Projects.Commands;
using Application.Projects.Responses;
using Application.Routes.Projects;
using Microsoft.AspNetCore.Components;

namespace WebUI.Managers.Projects;

public interface IProjectManager
{
    Task<List<ProjectResponse>> GetAllAsync();
    Task<ProjectResponse> GetByIdAsync(int projectId);
    Task SaveAsync(AddEditProjectCommand command);
    Task DeleteAsync(int projectId);
    Task CompleteAsync(int projectId);
    Task ResetAsync(int projectId);
}

public class ProjectManager : IProjectManager
{
    private readonly HttpClient _httpClient;
    public ProjectManager(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ProjectResponse>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<ProjectResponse>>(ProjectEndpoints.GetAll);
    }

    public async Task<ProjectResponse> GetByIdAsync(int projectId)
    {
        return await _httpClient.GetFromJsonAsync<ProjectResponse>(ProjectEndpoints.GetById(projectId));
    }
    public async Task SaveAsync(AddEditProjectCommand command)
    {
        var result = await _httpClient.PostAsJsonAsync(ProjectEndpoints.Edit, command);
    }
    public async Task DeleteAsync(int projectId)
    {
        var result = await _httpClient.DeleteAsync(ProjectEndpoints.Delete(projectId));
    }

    public async Task CompleteAsync(int projectId)
    {
        var result = await _httpClient.PostAsync(ProjectEndpoints.Delete(projectId), null);
    }

    public async Task ResetAsync(int projectId)
    {
        var result = await _httpClient.PostAsync(ProjectEndpoints.ResetComplete(projectId), null);
    }
}