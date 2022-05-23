using System.Text.Json.Serialization;
namespace Application.Identity.Responses;

public class LoginResponse
{
    public bool IsSuccess { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public string ErrorMessage { get; set; }
}