using Application.Identity.Commands;
using Application.Identity.Responses;
using LanguageExt;

namespace Application.Common.Interfaces.Identity;

public interface IUserService
{
    Task<Option<List<UserResponse>>> GetAllAsync();
    Task<Option<UserResponse>> GetAsync(string userId);
    Task<Option<List<UserRoleResponse>>> GetRolesAsync(string userId);
    Task SetActiveStatusAsync(SetActiveStatusCommand statusRequest);
    Task UpdateUserRolesAsync(UpdateUserRolesCommand userRolesRequest);
}