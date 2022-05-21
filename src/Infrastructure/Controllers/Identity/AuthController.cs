using Application.Common.Interfaces.Identity;
using Application.Identity.Commands;
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