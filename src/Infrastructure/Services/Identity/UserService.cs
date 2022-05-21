using Application.Common.Interfaces.Identity;
using Application.Identity.Commands;
using Application.Identity.Responses;
using AutoMapper;
using Infrastructure.Models;
using LanguageExt;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Identity;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(UserManager<ApplicationUser> userManager, IMapper mapper, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _mapper = mapper;
        _roleManager = roleManager;
    }

    public async Task<Option<List<UserResponse>>> GetAllAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        var result = _mapper.Map<List<UserResponse>>(users);
        return result;
    }

    public async Task<Option<UserResponse>> GetAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        var result = _mapper.Map<UserResponse>(user);
        return result;
    }

    public async Task<Option<List<UserRoleResponse>>> GetRolesAsync(string userId)
    {
        var result = new List<UserRoleResponse>();
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null) return null;
        var roles = await _roleManager.Roles.ToListAsync();
        foreach (var role in roles)
            result.Add(new UserRoleResponse
            {
                RoleName = role.Name,
                Selected = await _userManager.IsInRoleAsync(user, role.Name)
            });
        return result;
    }

    public async Task SetActiveStatusAsync(SetActiveStatusCommand statusRequest)
    {
        var user = await _userManager.FindByIdAsync(statusRequest.UserId);
        if (user is not null)
        {
            user.IsActive = statusRequest.IsActive;
            await _userManager.UpdateAsync(user);
        }
    }

    public async Task UpdateUserRolesAsync(UpdateUserRolesCommand userRolesRequest)
    {
        var user = await _userManager.FindByIdAsync(userRolesRequest.UserId);
        var roles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, roles);
        await _userManager.AddToRolesAsync(user,
            userRolesRequest.Roles.Where(r => r.IsSelected).Select(r => r.RoleName).ToList());
    }
}