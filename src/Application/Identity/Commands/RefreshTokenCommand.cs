namespace Application.Identity.Commands;

public class RefreshTokenCommand
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}