using Domain.Enums;

namespace Application.Projects.Responses;

public class TicketResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ProjectResponse Project { get; set; }
    public DateTime StartDate { get; set; } 
    public DateTime CompletedDate { get; set; }
    public Priority Priority { get; set; }
    public List<string> UserIds { get; set; }
}