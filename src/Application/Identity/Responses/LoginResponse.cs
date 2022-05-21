namespace Application.Identity.Responses;

public class LoginResponse
{
    public bool IsSuccess { get; set; }
    public string Token { get; set; }
    public string ErrorMessage { get; set; }
}