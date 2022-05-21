using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common.Interfaces.Identity;
using Application.Identity.Commands;
using Application.Identity.Responses;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services.Identity;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly IConfigurationSection _jwtSettings;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
        SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _jwtSettings = _configuration.GetSection("JwtSettings");
    }

    public async Task<LoginResponse> LoginAsync(LoginCommand request)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user == null) return new LoginResponse {ErrorMessage = "User not found!"};
        if (!user.EmailConfirmed) return new LoginResponse {ErrorMessage = "Email not confirmed!"};
        var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!passwordValid) return new LoginResponse {ErrorMessage = "Invalid credentials"};
        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaims(user);
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
        var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        return new LoginResponse
        {
            Token = token,
            IsSuccess = true
        };
    }

    public async Task<RegisterResponse> RegisterAsync(RegisterCommand? request)
    {
        var user = new ApplicationUser
        {
            UserName = request.UserName,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName
        };
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);
            return new RegisterResponse {Errors = errors};
        }

        return new RegisterResponse {IsSuccess = true};
    }

    private SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(_jwtSettings["securityKey"]);
        var secret = new SymmetricSecurityKey(key);

        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    public async Task<IEnumerable<Claim>> GetClaims(ApplicationUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);
        var roleClaims = new List<Claim>();
        var permissionClaims = new List<Claim>();
        foreach (var role in roles)
        {
            roleClaims.Add(new Claim(ClaimTypes.Role, role));
            var thisRole = await _roleManager.FindByNameAsync(role);
            var allPermissionsForThisRoles = await _roleManager.GetClaimsAsync(thisRole);
            permissionClaims.AddRange(allPermissionsForThisRoles);
        }

        var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Email, user.Email)
            }
            .Union(userClaims)
            .Union(roleClaims)
            .Union(permissionClaims);

        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
    {
        var tokenOptions = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings["expiryInMinutes"])),
            signingCredentials: signingCredentials);

        return tokenOptions;
    }
}