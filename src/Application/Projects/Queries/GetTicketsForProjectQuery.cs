using System.ComponentModel.DataAnnotations;
using Application.Common.Interfaces.Repository;
using Application.Common.Interfaces.Services.Identity;
using Application.Projects.Responses;
using AutoMapper;
using Domain.Entities.Projects;
using MediatR;

namespace Application.Projects.Queries;

public class GetTicketsForProjectQuery : IRequest<List<TicketResponse>>
{
    [Required]
    public int ProjectId { get; set; }
}

public class GetTicketsForProjectQueryHandler : IRequestHandler<GetTicketsForProjectQuery, List<TicketResponse>>
{
    private readonly IRepository<Project, int> _repository;
    private readonly ITicketRepository _ticketRepository;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    public GetTicketsForProjectQueryHandler(IUserService userService, IRepository<Project, int> repository, IMapper mapper, ITicketRepository ticketRepository)
    {
        _userService = userService;
        _repository = repository;
        _mapper = mapper;
        _ticketRepository = ticketRepository;
    }
    
    public async Task<List<TicketResponse>> Handle(GetTicketsForProjectQuery request, CancellationToken cancellationToken)
    {
        var tickets = await _ticketRepository.GetTicketsForProject(request.ProjectId);
        var mappedTickets = _mapper.Map<List<TicketResponse>>(tickets);
        return mappedTickets;
    }
}