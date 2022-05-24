using Application.Common.Interfaces.Identity;
using Application.Common.Interfaces.Repository;
using Application.Identity.Responses;
using Application.Projects.Responses;
using AutoMapper;
using Domain.Entities.Projects;
using FluentResults;
using LanguageExt;
using MediatR;

namespace Application.Projects.Queries;

public class GetAllProjectsQuery : IRequest<List<ProjectResponse>>
{

}

internal class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, List<ProjectResponse>>
{
    private readonly IRepository<Project, int> _repository;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    public GetAllProjectsQueryHandler(IRepository<Project, int> repository, IMapper mapper, IUserService userService)
    {
        _repository = repository;
        _mapper = mapper;
        _userService = userService;
    }
    public async Task<List<ProjectResponse>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects = await _repository.GetAllAsync();
        var mappedProjects = _mapper.Map<List<ProjectResponse>>(projects);
        foreach (ProjectResponse p in mappedProjects)
        {
            p.ProjectManager = await _userService.GetAsync(p.ProjectManagerId).Match(x => x, () => null!);
        }
        return mappedProjects;
    }
}