using Application.Projects.Commands;
using Application.Projects.Responses;
using Domain.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using WebUI.Managers.Projects;
using WebUI.Pages.Projects.Components;
using WebUI.Shared.Components;

namespace WebUI.Pages.Projects;

public partial class ProjectDetail
{
    private ProjectResponse _project;
    private List<TicketResponse> _ticketList;
    [Parameter] public int projectId { get; set; }
    [Inject] private IProjectManager _projectManager { get; set; }
    [Inject] private ITicketManager _ticketManager { get; set; }
    [Inject] private IDialogService _dialogService { get; set; }
    private bool HasTickets => _ticketList is not null && _ticketList.Count > 0;

    protected override async Task OnInitializedAsync()
    {
        await RefreshProject();
    }

    private async Task RefreshProject()
    {
        _project = await _projectManager.GetByIdAsync(projectId);
        _ticketList = await _ticketManager.GetByProject(projectId);
        StateHasChanged();
    }

    private async Task ItemUpdated(MudItemDropInfo<TicketResponse> ticket)
    {
        ticket.Item.Status = (Status) Enum.Parse(typeof(Status), ticket.DropzoneIdentifier);
        var ticketToUpdate = new AddEditTicketCommand
        {
            Description = ticket.Item.Description,
            Id = ticket.Item.Id,
            Name = ticket.Item.Name,
            Priority = ticket.Item.Priority,
            ProjectId = _project.Id,
            StartDate = ticket.Item.StartDate,
            Status = ticket.Item.Status,
            UserIds = ticket.Item.UserIds
        };
        await _ticketManager.SaveAsync(ticketToUpdate);
    }
    private async Task AddEditModal(int id = 0)
    {
        DialogParameters parameters = new DialogParameters();
        if (id != 0)
        {
            TicketResponse p = _ticketList.FirstOrDefault(p => p.Id == id);
            if (p is not null)
            {
                parameters.Add(nameof(AddEditTicketModal._editTicketCommand), new AddEditTicketCommand()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    StartDate = p.StartDate,
                    Priority = p.Priority,
                    UserIds = p.UserIds.ToList()
                });
            }
        }

        var dialog = _dialogService.Show<AddEditTicketModal>(id == 0 ? "Create" : "Edit", parameters,
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
            await RefreshProject();
        }
    }

    private async Task DeleteModal(int id)
    {
        DialogParameters parameters = new DialogParameters();
        parameters.Add(nameof(DialogConfirmation.Color), Color.Error);
        parameters.Add(nameof(DialogConfirmation.ButtonText), "Submit");
        parameters.Add(nameof(DialogConfirmation.ContentText), "Are you sure you want to delete ticket?");
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
            await _ticketManager.DeleteAsync(id);
            await RefreshProject();
        }
    }
}