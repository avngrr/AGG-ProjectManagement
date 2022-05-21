using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Models;

public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public bool? IsActive { get; set; } = true;
}