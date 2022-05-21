using System.ComponentModel.DataAnnotations;

namespace Application.Identity.Commands;

public class LoginCommand
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}