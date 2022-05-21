using Application.Common.Interfaces.Repository;
using Application.Projects.Responses;
using AutoMapper;
using Domain.Entities.Projects;
using MediatR;

namespace Application.Projects.Queries;

public class GetAllProjectsCommand : IRequest<List<ProjectResponse>>
{

}

internal class GetAllProjectsCommandHandler : IRequestHandler<GetAllProjectsCommand, List<ProjectResponse>>
{
    private readonly IRepository<Project, int> _repository;
    private readonly IMapper _mapper;
    public GetAllProjectsCommandHandler(IRepository<Project, int> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<List<ProjectResponse>> Handle(GetAllProjectsCommand request, CancellationToken cancellationToken)
    {
        var projects = await _repository.GetAllAsync();
        var mappedProjects = _mapper.Map<List<ProjectResponse>>(projects);
        return mappedProjects;
    }
}