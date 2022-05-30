using Application.Common.Interfaces.Repository;
using Domain.Entities.Projects;
using Domain.Enums;
using FluentResults;
using MediatR;

namespace Application.Projects.Commands;

public class ReopenTicketCommand : IRequest<Result<string>>
{
    public int Id { get; set; }
}

public class ReopenTicketCommandHandler : IRequestHandler<ReopenTicketCommand, Result<string>>
{
    private readonly IRepository<Ticket, int> _repository;

    public ReopenTicketCommandHandler(IRepository<Ticket, int> repository)
    {
        _repository = repository;
    }
    public async Task<Result<string>> Handle(ReopenTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _repository.GetByIdAsync(request.Id);
        if (ticket is null)
        {
            return Result.Fail("Ticket not found!");
        }
        ticket.CompletedDate = DateTime.MinValue;
        ticket.Status = Status.ToDo;
        await _repository.UpdateAsync(ticket);
        await _repository.Save();
        return Result.Ok("Returned completed ticket to incomplete!");
    }
}