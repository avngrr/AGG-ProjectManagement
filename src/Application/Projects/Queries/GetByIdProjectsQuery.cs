using System.ComponentModel.DataAnnotations;
using Application.Common.Interfaces.Identity;
using Application.Common.Interfaces.Repository;
using Application.Projects.Responses;
using AutoMapper;
using Domain.Entities.Projects;
using LanguageExt;
using MediatR;

namespace Application.Projects.Queries;

public class GetByIdProjectsQuery : IRequest<Option<ProjectResponse>>
{
    [Required]
    public int Id { get; set; }
}

internal class GetByIdProjectsQueryHandler : IRequestHandler<GetByIdProjectsQuery, Option<ProjectResponse>>
{
    private readonly IRepository<Project, int> _repository;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    public GetByIdProjectsQueryHandler(IRepository<Project, int> repository, IMapper mapper, IUserService userService)
    {
        _repository = repository;
        _mapper = mapper;
        _userService = userService;
    }
    public async Task<Option<ProjectResponse>> Handle(GetByIdProjectsQuery request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetByIdAsync(request.Id);
        var mappedProject = _mapper.Map<ProjectResponse>(project);
        mappedProject.ProjectManager = await _userService.GetAsync(mappedProject.ProjectManagerId).Match(x => x, () => null!);
        return mappedProject;
    }
}