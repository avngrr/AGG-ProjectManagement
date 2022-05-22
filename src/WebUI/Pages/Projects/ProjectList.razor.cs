using Application.Projects.Responses;
using Microsoft.AspNetCore.Components;
using WebUI.Managers.Projects;

namespace WebUI.Pages.Projects;

public partial class ProjectList
{
    private List<ProjectResponse> _projects;
    private bool IsLoaded => _projects is not null && _projects.Count > 0;
    [Inject] private IProjectManager _projectManager { get; set; }
    private string _searchString = "";

    protected override async Task OnInitializedAsync()
    {
        _projects = await _projectManager.GetAllAsync();
    }

    private void OnSearch(string s)
    {
        _searchString = s;
    }
}