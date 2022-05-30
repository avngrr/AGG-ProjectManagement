using System.Reflection;
using System.Security.Claims;
using Application.Common.Interfaces.Services.Identity;
using Application.Common.Permissions;
using Application.Identity.Commands;
using Application.Identity.Responses;
using AutoMapper;
using LanguageExt;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Identity;

public class RoleService : IRoleService
{
    private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        public RoleService(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<Option<string>> DeleteAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            await _roleManager.DeleteAsync(role);
            return null;
        }

        public async Task<Option<List<RoleResponse>>> GetAllAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var result = _mapper.Map<List<RoleResponse>>(roles);
            return result;
        }

        public async Task<Option<PermissionResponse>> GetAllPermissionsAsync(string roleId)
        {
            var allPermissions = typeof(Permissions).GetNestedTypes().SelectMany(c => c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy));
            var role = await _roleManager.FindByIdAsync(roleId);
            var model = new PermissionResponse();
            if (role is not null)
            {
                model.RoleId = role.Id;
                model.RoleName = role.Name;
                var currentClaims = await _roleManager.GetClaimsAsync(role);
                foreach (var claim in allPermissions)
                {
                    var value = claim.GetValue(null);
                    model.RoleClaims.Add(new RoleClaimResponse()
                    {
                        Value = value.ToString(),
                        Type = "Permission",
                        Selected = currentClaims.Any(c => c.Value == value.ToString())
                    });
                }
            }
            return model;
        }

        public async Task<Option<RoleResponse>> GetByIdAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var result = _mapper.Map<RoleResponse>(role);
            return result;
        }

        public async Task<Option<string>> SaveAsync(RoleCommand request)
        {
            var role = await _roleManager.FindByNameAsync(request.RoleName);
            if (role == null)
            {
                await _roleManager.CreateAsync(new IdentityRole(request.RoleName));
                return null;
            }
            return "Role allready exists!";
        }

        public async Task<Option<string>> UpdatePermissionsAsync(PermissionCommand request)
        {
            var role = await _roleManager.FindByIdAsync(request.RoleId);
            var currentClaims = await _roleManager.GetClaimsAsync(role);
            foreach (var claim in currentClaims)
            {
                await _roleManager.RemoveClaimAsync(role, claim);
            }
            foreach (var claim in request.RoleClaims)
            {
                await _roleManager.AddClaimAsync(role, new Claim(claim.Type, claim.Value));
            }
            return null;
        }
}