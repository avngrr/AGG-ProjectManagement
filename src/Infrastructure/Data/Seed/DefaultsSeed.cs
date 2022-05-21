using System.Security.Claims;
using Application.Common.Permissions;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Infrastructure.Data.Seed;

public static class DefaultsSeed
{
    private static int _roleClaimId = 1;
    private static string _adminId = "0b700e63-780c-488a-bd56-de61403d5a0f";
    private static string _adminRoleId = "f073ed4d-6b92-411d-bcca-1911b4ccd365";
    private static string _developerRoleId = "50df68f2-75fc-4773-aec0-e1b7fd0749ff";
    private static string _basicRoleId = "16cc9f16-3765-45b0-ba3d-eb5cecd51fed";
    public static ModelBuilder AddDefaultUsersRoles(this ModelBuilder builder)
    {
        ApplicationUser admin = new ApplicationUser()
        {
            Id = _adminId,
            UserName = "Admin",
            NormalizedUserName = "Admin",
            Email = "admin@gmail.com",
            NormalizedEmail = "admin@gmail.com",
            IsActive = true,
            EmailConfirmed = true
        };
        PasswordHasher<ApplicationUser> hasher = new PasswordHasher<ApplicationUser>();
        admin.PasswordHash = hasher.HashPassword(admin, "COM123puter!");
        builder.Entity<ApplicationUser>().HasData(admin);

        builder.Entity<IdentityRole>().HasData(
            new IdentityRole()
            {
                Id = _adminRoleId,
                Name = "Admin",
                ConcurrencyStamp = "1",
                NormalizedName = "Admin"
            },
            new IdentityRole()
            {
                Id = _developerRoleId,
                Name = "Developer",
                ConcurrencyStamp = "2",
                NormalizedName = "Developer"
            },
            new IdentityRole()
            {
                Id = _basicRoleId,
                Name = "Basic",
                ConcurrencyStamp = "3",
                NormalizedName = "Basic"
            }
        );
        AddPermissionClaims(builder, _adminRoleId, "Users");
        AddPermissionClaims(builder, _adminRoleId, "Roles");
        AddPermissionClaims(builder, _adminRoleId, "Projects");
        builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        {
            RoleId = _adminRoleId,
            UserId = _adminId
        });
        
        return builder;
    }

    private static void AddPermissionClaims(ModelBuilder builder, string roleId, string moduleName)
    {
        var allPermissions = Permissions.GeneratePermissionsForModule(moduleName);
        foreach (var permission in allPermissions)
        {
            builder.Entity<IdentityRoleClaim<string>>().HasData(
                new IdentityRoleClaim<string>()
                {
                    ClaimType = "Permission",
                    ClaimValue = permission,
                    RoleId = roleId,
                    Id = _roleClaimId
                }
            );
            _roleClaimId++;
        }
    }

}