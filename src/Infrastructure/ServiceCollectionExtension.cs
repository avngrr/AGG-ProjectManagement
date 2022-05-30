using System.ComponentModel;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using Application.Common.Interfaces.Repository;
using Application.Common.Interfaces.Services.Identity;
using Application.Common.Permissions;
using Infrastructure.Data;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.Services.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Infrastructure;

public static class ServiceCollectionExtension
{
    internal static IServiceCollection AddInfrastructureMappings(this IServiceCollection services)
    {
        return services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }
    internal static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "API",
                Version = "v2",
                Description = "Your Api Description"
            });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        return services;
    }
    internal static IServiceCollection AddServerServices(this IServiceCollection services)
    {
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IRoleService, RoleService>();
        return services;
    }
    internal static IServiceCollection AddJwtSettings(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JWTSettings");
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["securityKey"]))
            };
        });
        return services;
    }
    internal static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        return services;
    }
    internal static IServiceCollection AddAuthorizationPermissions(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            // Here I stored necessary permissions/roles in a constant
            foreach (var prop in typeof(Permissions).GetNestedTypes().SelectMany(c =>
                         c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)))
            {
                var propertyValue = prop.GetValue(null);
                if (propertyValue is not null)
                    options.AddPolicy(propertyValue.ToString(), policy =>
                    {
                        policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                        policy.RequireAuthenticatedUser();
                        policy.RequireClaim("Permission", propertyValue.ToString());
                    });
            }

            options.AddPolicy("isAdmin", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
        });
        return services;
    }

    internal static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<ITicketRepository, TicketRepository>();
        return services.AddTransient(typeof(IRepository<,>), typeof(Repository<,>));
    }
    internal static IServiceCollection AddVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
        });
        return services;
    }
}