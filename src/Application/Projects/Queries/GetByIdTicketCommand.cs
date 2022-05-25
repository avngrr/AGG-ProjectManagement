using Application.Common.Interfaces.Repository;
using Application.Projects.Responses;
using AutoMapper;
using Domain.Entities.Projects;
using MediatR;

namespace Application.Projects.Queries;

public class GetByIdTicketCommand : IRequest<TicketResponse>
{
    public int Id { get; set; }
}

internal class GetByIdTicketCommandHandler : IRequestHandler<GetByIdTicketCommand, TicketResponse>
{
    private readonly IRepository<Ticket, int> _repository;
    private readonly IMapper _mapper;
    public GetByIdTicketCommandHandler(IRepository<Ticket, int> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<TicketResponse> Handle(GetByIdTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _repository.GetByIdAsync(request.Id);
        var mappedTicket = _mapper.Map<TicketResponse>(ticket);
        return mappedTicket;
    }
}