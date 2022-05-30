using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Common.Interfaces.Services.Identity;
using Application.Identity.Commands;
using Application.Identity.Responses;
using Infrastructure.Data;
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
    private readonly ApplicationDbContext _context;

    public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
        SignInManager<ApplicationUser> signInManager, IConfiguration configuration, ApplicationDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _context = context;
        _jwtSettings = _configuration.GetSection("JwtSettings");
    }

    public async Task<LoginResponse> LoginAsync(LoginCommand request)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user == null) return new LoginResponse {ErrorMessage = "User not found!"};
        if (!user.EmailConfirmed) return new LoginResponse {ErrorMessage = "Email not confirmed!"};
        var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!passwordValid) return new LoginResponse {ErrorMessage = "Invalid credentials"};
        var token = await generateJwtToken(user);
        user.RefreshToken = GenerateRefreshToken();
        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
        await _userManager.UpdateAsync(user);
        return new LoginResponse
        {
            Token = token,
            RefreshToken = user.RefreshToken,
            IsSuccess = true
        };
    }
    public async Task<LoginResponse> RefreshToken(RefreshTokenCommand command)
    {
        var user = _context.Users.SingleOrDefault(u => u.RefreshToken == command.RefreshToken);
            
        // return null if no user found with token
        if (user == null) return null;

        // replace old refresh token with a new one and save

        user.RefreshToken = GenerateRefreshToken();
        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
        await _userManager.UpdateAsync(user);

        // generate new jwt
        var newJwtToken = await generateJwtToken(user);
        return new LoginResponse
        {
            Token = newJwtToken,
            RefreshToken = user.RefreshToken,
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

    private async Task<IEnumerable<Claim>> GetClaims(ApplicationUser user)
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

    private async Task<string> generateJwtToken(ApplicationUser user)
    {
        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaims(user);
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }
    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
    {
        var tokenOptions = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings["expiryInMinutes"])),
            signingCredentials: signingCredentials);

        return tokenOptions;
    }
    
    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}