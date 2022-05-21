namespace Application.Identity.Commands;

public class SetActiveStatusCommand
{
    public string UserId { get; set; }
    public bool IsActive { get; set; }
}