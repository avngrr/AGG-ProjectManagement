using Application.Common.Interfaces.Repository;
using AutoMapper;
using Domain.Entities.Projects;
using Domain.Enums;
using FluentResults;
using MediatR;

namespace Application.Projects.Commands;

public class AddEditTicketCommand : IRequest<Result<string>>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int ProjectId { get; set; }
    public DateTime? StartDate { get; set; } 
    public Priority Priority { get; set; } = Priority.MID;
    public Status Status { get; set; } = Status.ToDo;
    public List<string> UserIds { get; set; } = new List<string>();
}

internal class AddEditTicketCommandHandler : IRequestHandler<AddEditTicketCommand, Result<string>>
{
    private readonly IRepository<Ticket, int> _repository;
    private readonly IMapper _mapper;

    public AddEditTicketCommandHandler(IRepository<Ticket, int> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<string>> Handle(AddEditTicketCommand request, CancellationToken cancellationToken)
    {
        if (request.Id == 0)
        {
            var ticket = _mapper.Map<Ticket>(request);
            await _repository.AddAsync(ticket);
            await _repository.Save();
            return Result.Ok("Created new ticket!");
        }
        else
        {
            var ticket = await _repository.GetByIdAsync(request.Id);
            ticket.Description = request.Description;
            ticket.Name = request.Name;
            ticket.StartDate = request.StartDate ?? DateTime.MinValue;
            ticket.Priority = request.Priority;
            ticket.UserIds = request.UserIds.SequenceEqual(ticket.UserIds) ? ticket.UserIds : request.UserIds;
            ticket.ProjectId = request.ProjectId;
            ticket.Status = request.Status;
            await _repository.UpdateAsync(ticket);
            await _repository.Save();
            return Result.Ok("Updated ticket!");
        }
    }
}