using Application.Identity.Commands;
using Application.Identity.Responses;
using LanguageExt;

namespace Application.Common.Interfaces.Services.Identity;

public interface IRoleService
{
    Task<Option<string>> DeleteAsync(string id);
    Task<Option<List<RoleResponse>>> GetAllAsync();
    Task<Option<PermissionResponse>> GetAllPermissionsAsync(string roleId);
    Task<Option<RoleResponse>> GetByIdAsync(string id);
    Task<Option<string>> SaveAsync(RoleCommand request);
    Task<Option<string>> UpdatePermissionsAsync(PermissionCommand request);
}