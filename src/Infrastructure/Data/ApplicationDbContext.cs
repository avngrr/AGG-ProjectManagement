using Application.Common.Interfaces.Data;
using Domain.Entities;
using Domain.Entities.Projects;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure.Data;

public class ApplicationDbContext : AuditableContext
{
    public ApplicationDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
    {
    }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Project>(entity =>
        {
            entity.HasKey(project => project.Id);
        });
        builder.Entity<Ticket>(entity =>
        {
            entity.HasKey(ticket => ticket.Id);
        });
    }
}