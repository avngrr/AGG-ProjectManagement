using Application.Identity.Commands;
using Application.Identity.Responses;

namespace Application.Common.Interfaces.Authentication;

public interface IAuthenticationManager
{
    Task<RegisterResponse> RegisterAsync(RegisterCommand request);
    Task<LoginResponse> LoginAsync(LoginCommand userForAuthentication);
    Task LogoutAsync();
    Task<string> TryRefreshToken();
}