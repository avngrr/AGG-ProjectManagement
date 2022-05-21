using System.Text.Json;
using Domain.Entities.Projects;
using Duende.IdentityServer.EntityFramework.Options;
using Infrastructure.Data.Seed;
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
            entity.HasMany(project => project.Tickets)
                .WithOne(ticket => ticket.Project)
                .OnDelete(DeleteBehavior.NoAction);
            entity.Property(project => project.UserIds)
                .HasConversion(
                    s => JsonSerializer.Serialize(s, new JsonSerializerOptions()),
                    s => JsonSerializer.Deserialize<List<string>>(s, new JsonSerializerOptions()));
        });
        builder.Entity<Ticket>(entity =>
        {
            entity.HasKey(ticket => ticket.Id);
            entity.Property(project => project.UserIds)
                .HasConversion(
                    s => JsonSerializer.Serialize(s, new JsonSerializerOptions()),
                    s => JsonSerializer.Deserialize<List<string>>(s, new JsonSerializerOptions()));
        });
        builder.AddDefaultUsersRoles();
    }
}