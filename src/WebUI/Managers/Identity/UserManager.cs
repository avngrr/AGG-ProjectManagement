using System.Net.Http.Json;
using Application.Identity.Commands;
using Application.Identity.Responses;
using Application.Projects.Responses;
using Application.Routes.Identity;
using Application.Routes.Projects;

namespace WebUI.Managers.Identity;

public class UserManager
{
    private readonly HttpClient _httpClient;
    public UserManager(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<List<UserResponse>> GetAllAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<UserResponse>>(UserEndpoints.GetAll);
    }
    public async Task<UserResponse> GetByIdAsync(string userId)
    {
        return await _httpClient.GetFromJsonAsync<UserResponse>(UserEndpoints.Get(userId));
    }
    public async Task<UserResponse> GetRoles(string userId)
    {
        return await _httpClient.GetFromJsonAsync<UserResponse>(UserEndpoints.GetRoles(userId));
    }
    public async Task UpdateRoles(UpdateUserRolesCommand command)
    {
        var result = await _httpClient.PostAsJsonAsync(UserEndpoints.UpdateRoles, command);
    }
    public async Task SetActive(SetActiveStatusCommand command)
    {
        var result = await _httpClient.PostAsJsonAsync(UserEndpoints.SetActive, command);
    }
}