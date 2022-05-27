using Application.Common.Interfaces.Repository;
using Domain.Entities.Projects;
using FluentResults;
using MediatR;

namespace Application.Projects.Commands;

public class DeleteTicketCommand : IRequest<Result<string>>
{
    public int Id { get; set; }
}

public class DeleteTicketCommandHandler : IRequestHandler<DeleteTicketCommand, Result<string>>
{
    private readonly IRepository<Ticket, int> _repository;
    public DeleteTicketCommandHandler(IRepository<Ticket, int> repository)
    {
        _repository = repository;
    }

    public async Task<Result<string>> Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _repository.GetByIdAsync(request.Id);
        if (ticket is null)
        {
            return Result.Fail("Ticket not found!");
        }
        await _repository.DeleteAsync(ticket);
        await _repository.Save();
        return Result.Ok("Deleted ticket!");
    }
}