using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain.Entities.Projects;

public class Ticket : AuditableEntity<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int ProjectId { get; set; }
    [ForeignKey(nameof(ProjectId))]
    public virtual Project Project { get; set; }
    public DateTime StartDate { get; set; } 
    public DateTime CompletedDate { get; set; }
    public Priority Priority { get; set; } = Priority.MID;
    public virtual List<string> UserIds { get; set; } = new List<string>();
}