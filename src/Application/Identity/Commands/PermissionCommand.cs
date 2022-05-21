namespace Application.Identity.Commands;

public class PermissionCommand
{
    public string RoleId { get; set; }
    public List<RoleClaimCommand> RoleClaims { get; set; }
}