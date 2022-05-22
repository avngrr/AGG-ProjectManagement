using Application.Projects.Commands;
using Application.Projects.Responses;
using Domain.Entities.Projects;
using LanguageExt.ClassInstances;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using WebUI.Managers.Projects;
using WebUI.Pages.Projects.Components;
using WebUI.Shared.Components;

namespace WebUI.Pages.Projects;

public partial class ProjectList
{
    private List<ProjectResponse> _projects;
    private bool IsLoaded => _projects is not null && _projects.Count > 0;
    [Inject] private IDialogService _dialogService { get; set; }
    [Inject] private IProjectManager _projectManager { get; set; }
    private string _searchString = "";

    protected override async Task OnInitializedAsync()
    {
        await Filter();
    }
    private async Task Filter()
    {
        _projects = await _projectManager.GetAllAsync();
        if (!string.IsNullOrWhiteSpace(_searchString))
        {
            _projects = _projects.Where(
                p => p.Name.Contains(_searchString, StringComparison.InvariantCultureIgnoreCase)
            ).ToList();
        }
        StateHasChanged();
    }
    private async Task OnSearch(string s = "")
    {
        _searchString = s;
        await Filter();
    }

    private async Task AddEditModal(int id = 0)
    {
        DialogParameters parameters = new DialogParameters();
        if (id != 0)
        {
            ProjectResponse p = _projects.FirstOrDefault(p => p.Id == id);
            if (p is not null)
            {
                parameters.Add(nameof(AddEditProjectModal._editProjectCommand), new AddEditProjectCommand()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    StartDate = p.StartDate,
                    ProjectManagerId = p.ProjectManagerId,
                    UserIds = p.UserIds.ToList()
                });
            }
        }

        var dialog = _dialogService.Show<AddEditProjectModal>(id == 0 ? "Create" : "Edit", parameters,
            new DialogOptions()
            {
                CloseButton = true,
                MaxWidth = MaxWidth.Medium,
                FullWidth = true,
                DisableBackdropClick = true
            });
        var result = await dialog.Result;
        if (result != DialogResult.Cancel())
        {
            await OnSearch();
        }
    }

    private async Task Delete(int id)
    {
        DialogParameters parameters = new DialogParameters();
        parameters.Add(nameof(DialogConfirmation.Color), Color.Error);
        parameters.Add(nameof(DialogConfirmation.ButtonText), "Submit");
        parameters.Add(nameof(DialogConfirmation.ContentText), "Are you sure you want to delete project?");
        var dialog = _dialogService.Show<DialogConfirmation>("Delete", parameters, new DialogOptions()
        {
            CloseButton = true,
            MaxWidth = MaxWidth.Small,
            FullScreen = false,
            DisableBackdropClick = true
        });
        var result = await dialog.Result;
        if (result != DialogResult.Cancel())
        {
            await _projectManager.DeleteAsync(id);
            await OnSearch();
        }
    }
}