using Application.Common.Interfaces.Repository;
using Domain.Entities.Projects;
using Domain.Enums;
using FluentResults;
using MediatR;

namespace Application.Projects.Commands;

public class CompleteTicketCommand : IRequest<Result<string>>
{
    public int Id { get; set; }
}
public class CompleteTicketCommandHandler : IRequestHandler<CompleteTicketCommand, Result<string>>
{
    private readonly IRepository<Ticket, int> _repository;

    public CompleteTicketCommandHandler(IRepository<Ticket, int> repository)
    {
        _repository = repository;
    }
    public async Task<Result<string>> Handle(CompleteTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _repository.GetByIdAsync(request.Id);
        if (ticket is null)
        {
            return Result.Fail("Ticket not found!");
        }
        ticket.CompletedDate = DateTime.Now;
        ticket.Status = Status.Done;
        await _repository.UpdateAsync(ticket);
        await _repository.Save();
        return Result.Ok("Ticket set to complete!");
    }
}
