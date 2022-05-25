using Domain.Entities.Projects;

namespace Application.Common.Interfaces.Repository;

public interface ITicketRepository
{
    Task<List<Ticket>> GetTicketsForProject(int projectId);
}