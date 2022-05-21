using Application.Common.Interfaces.Identity;
using Application.Common.Permissions;
using Application.Identity.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Controllers.Identity;

[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;
    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }
    [Authorize(Policy = Permissions.Roles.View)]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var roles = await _roleService.GetAllAsync();
        return roles.Match<IActionResult>(Ok, NotFound);
    }
    [Authorize(Policy = Permissions.Roles.Create)]
    [HttpPost]
    public async Task<IActionResult> Post(RoleCommand command)
    {
        var result = await _roleService.SaveAsync(command);
        return result.Match<IActionResult>(Ok, NotFound);
    }
    [Authorize(Policy = Permissions.Roles.Delete)]
    [HttpDelete("{roleId}")]
    public async Task<IActionResult> Delete(string roleId)
    {
        var result = await _roleService.DeleteAsync(roleId);
        return result.Match<IActionResult>(Ok, NotFound);
    }
    [Authorize(Policy = Permissions.Roles.View)]
    [HttpGet("permissions/{roleId}")]
    public async Task<IActionResult> GetPermissionsByRoleId(string roleId)
    {
        var result = await _roleService.GetAllPermissionsAsync(roleId);
        return result.Match<IActionResult>(Ok, NotFound);
    }
    [Authorize(Policy = Permissions.Roles.Edit)]
    [HttpPut("permissions/update")]
    public async Task<IActionResult> Update(PermissionCommand command)
    {
        var result = await _roleService.UpdatePermissionsAsync(command);
        return result.Match<IActionResult>(Ok, NotFound);
    }
}