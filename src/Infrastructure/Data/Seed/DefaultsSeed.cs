using System.Security.Claims;
using Application.Common.Permissions;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Infrastructure.Data.Seed;

public static class DefaultsSeed
{
    private static int roleClaimId = 1;
    public static ModelBuilder AddDefaultUsersRoles(this ModelBuilder builder)
    {
        string AdminId = Guid.NewGuid().ToString();
        string AdminRoleId = Guid.NewGuid().ToString();
        ApplicationUser admin = new ApplicationUser()
        {
            Id = AdminId.ToString(),
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
                Id = AdminRoleId,
                Name = "Admin",
                ConcurrencyStamp = "1",
                NormalizedName = "Admin"
            },
            new IdentityRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Developer",
                ConcurrencyStamp = "2",
                NormalizedName = "Developer"
            },
            new IdentityRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Basic",
                ConcurrencyStamp = "3",
                NormalizedName = "Basic"
            }
        );
        AddPermissionClaims(builder, AdminRoleId, "Users");
        AddPermissionClaims(builder, AdminRoleId, "Roles");
        AddPermissionClaims(builder, AdminRoleId, "Projects");
        builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        {
            RoleId = AdminRoleId,
            UserId = AdminId
        });
        
        return builder;
    }

    public static void AddPermissionClaims(ModelBuilder builder, string roleId, string moduleName)
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
                    Id = roleClaimId
                }
            );
            roleClaimId++;
        }
    }

}