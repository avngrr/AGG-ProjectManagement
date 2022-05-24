using Application.Identity.Responses;

namespace Application.Projects.Responses;

public class ProjectResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ProjectManagerId { get; set; }
    public UserResponse ProjectManager { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? CompleteData { get; set; }
    public bool IsDeleted { get; set; }
    public IEnumerable<TicketResponse> Tickets { get; set; }
    public IEnumerable<string> UserIds { get; set; }
}