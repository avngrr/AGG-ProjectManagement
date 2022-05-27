using Application.Projects.Commands;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using WebUI.Managers.Projects;

namespace WebUI.Pages.Projects.Components;

public partial class AddEditTicketModal
{
    [Inject] public ITicketManager _TicketManager { get; set; }
    [Parameter] public AddEditTicketCommand _editTicketCommand { get; set; } = new();
    [CascadingParameter] MudDialogInstance MudDialog { get; set; }
    private async Task SaveAsync()
    {
        await _TicketManager.SaveAsync(_editTicketCommand);
        MudDialog.Close();
    }
}