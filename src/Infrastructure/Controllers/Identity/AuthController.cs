using Application.Common.Interfaces.Services.Identity;
using Application.Identity.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Controllers.Identity;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginCommand request)
    {
        var result = await _authService.LoginAsync(request);
        if (string.IsNullOrEmpty(result.ErrorMessage))
        {
            return Ok(result);
        }
        else
        {
            return Unauthorized(result);
        }
    }
    [AllowAnonymous]
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken(RefreshTokenCommand command)
    {
        var response = await _authService.RefreshToken(command);

        if (response == null)
            return Unauthorized(new { message = "Invalid token" });
        return Ok(response);
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand? request)
    {
        if (request == null) return BadRequest();
        var result = await _authService.RegisterAsync(request);
        if (result.Errors is not null)
        {
            return BadRequest(result);
        }
        else
        {
            return StatusCode(201);
        }
    }
}