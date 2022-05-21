using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Projects;

public class Project : AuditableEntity<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ProjectManagerId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? CompleteData { get; set; }
    public bool IsDeleted { get; set; }
    public virtual List<Ticket> Tickets { get; set; } = new List<Ticket>();
    public virtual List<string> UserIds { get; set; } = new List<string>();
}