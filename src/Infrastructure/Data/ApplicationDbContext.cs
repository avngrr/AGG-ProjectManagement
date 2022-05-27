using System.Security.Claims;
using System.Text.Json;
using Domain.Entities;
using Domain.Entities.Projects;
using Duende.IdentityServer.EntityFramework.Options;
using Infrastructure.Data.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure.Data;

public class ApplicationDbContext : AuditableContext
{
    private readonly string _userId;
    public ApplicationDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions, IHttpContextAccessor accessor) : base(options, operationalStoreOptions)
    {
        _userId = accessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
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

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity<int>>().ToList())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedOn = DateTime.UtcNow;
                    entry.Entity.CreatedBy = _userId;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedOn = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy = _userId;
                    break;
            }
        }
        return await base.SaveChangesAsync(_userId, cancellationToken);
    }
}