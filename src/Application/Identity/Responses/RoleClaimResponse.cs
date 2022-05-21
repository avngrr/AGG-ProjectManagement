namespace Application.Identity.Responses;

public class RoleClaimResponse
{
    public int Id { get; set; }
    public string RoleId { get; set; }
    public string Type { get; set; }
    public string Value { get; set; }
    public bool Selected { get; set; }
}