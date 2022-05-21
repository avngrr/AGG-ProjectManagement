namespace Application.Identity.Responses;

public class RegisterResponse
{
    public bool IsSuccess { get; set; }
    public IEnumerable<string> Errors { get; set; } = new List<string>();
}