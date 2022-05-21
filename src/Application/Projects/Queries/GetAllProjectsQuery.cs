using Application.Common.Interfaces.Repository;
using Application.Projects.Responses;
using AutoMapper;
using Domain.Entities.Projects;
using MediatR;

namespace Application.Projects.Queries;

public class GetAllProjectsQuery : IRequest<List<ProjectResponse>>
{

}

internal class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, List<ProjectResponse>>
{
    private readonly IRepository<Project, int> _repository;
    private readonly IMapper _mapper;
    public GetAllProjectsQueryHandler(IRepository<Project, int> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<List<ProjectResponse>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects = await _repository.GetAllAsync();
        var mappedProjects = _mapper.Map<List<ProjectResponse>>(projects);
        return mappedProjects;
    }
}