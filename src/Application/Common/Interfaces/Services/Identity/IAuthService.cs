using Application.Identity.Commands;
using Application.Identity.Responses;

namespace Application.Common.Interfaces.Services.Identity;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginCommand request);
    Task<RegisterResponse> RegisterAsync(RegisterCommand? request);
    Task<LoginResponse> RefreshToken(RefreshTokenCommand command);
}