using Application.Common.Interfaces.Repository;
using Domain.Entities.Projects;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<Ticket> _table;
    public TicketRepository(ApplicationDbContext context)
    {
        _context = context;
        _table = _context.Set<Ticket>();
    }

    public async Task<List<Ticket>> GetTicketsForProject(int projectId)
    {
        return await _context.Tickets.Where(t => t.ProjectId == projectId).ToListAsync();
    }
}