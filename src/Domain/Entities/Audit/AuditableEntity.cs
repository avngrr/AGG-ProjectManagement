﻿namespace Domain.Entities;

public abstract class AuditableEntity<TId>
{
    public TId Id;
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string LastModifiedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
}