namespace Application.Identity.Commands;

public class UpdateUserRolesCommand
{
    public string UserId { get; set; }
    public List<UserRoleSelected> Roles { get; set; }
}

public class UserRoleSelected
{
    public string RoleName { get; set; }
    public bool IsSelected { get; set; }
}