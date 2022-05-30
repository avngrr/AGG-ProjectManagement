using Application.Common.Interfaces.Services.Identity;
using Application.Common.Permissions;
using Application.Identity.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Controllers.Identity;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _userService.GetAllAsync();
        return result.Match<IActionResult>(Ok, NotFound);
    }

    [Authorize(Policy = Permissions.Users.View)]
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetAsync(string userId)
    {
        var result = await _userService.GetAsync(userId);
        return result.Match<IActionResult>(Ok, NotFound);
    }

    [Authorize(Policy = Permissions.Users.View)]
    [HttpGet("roles/{userId}")]
    public async Task<IActionResult> GetRolesAsync(string userId)
    {
        var result = await _userService.GetRolesAsync(userId);
        return result.Match<IActionResult>(Ok, NotFound);
    }

    [Authorize(Policy = Permissions.Users.Edit)]
    [HttpPut("roles/update")]
    public async Task<IActionResult> UpdateRolesAsync(UpdateUserRolesCommand request)
    {
        await _userService.UpdateUserRolesAsync(request);
        return Ok();
    }

    [Authorize(Policy = Permissions.Users.Edit)]
    [HttpPost("active")]
    public async Task<IActionResult> SetActiveStatusAsync(SetActiveStatusCommand request)
    {
        await _userService.SetActiveStatusAsync(request);
        return Ok();
    }
}