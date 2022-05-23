using Application.Identity.Commands;
using Application.Identity.Responses;

namespace Application.Common.Interfaces.Identity;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginCommand request);
    Task<RegisterResponse> RegisterAsync(RegisterCommand? request);
    Task<LoginResponse> RefreshToken(RefreshTokenCommand command);
}