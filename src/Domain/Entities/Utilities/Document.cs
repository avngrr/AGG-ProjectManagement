namespace Domain.Entities.Utilities;

public class Document : AuditableEntity<int>
{
    public string Name { get; set; }
    public bool IsPublic { get; set; }
    public string URL { get; set; }
    public DocumentType DocumentType { get; set; }
    public string ObjectId { get; set; }
}