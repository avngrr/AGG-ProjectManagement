using Application.Projects.Commands;
using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using WebUI.Managers.Projects;


namespace WebUI.Pages.Projects.Components;

public partial class AddEditProjectModal
{
    [Inject] public IProjectManager _ProjectManager { get; set; }
    [Parameter] public AddEditProjectCommand _editProjectCommand { get; set; } = new();
    [CascadingParameter] MudDialogInstance MudDialog { get; set; }
    private async Task SaveAsync()
    {
        await _ProjectManager.SaveAsync(_editProjectCommand);
        MudDialog.Close();
    }
}