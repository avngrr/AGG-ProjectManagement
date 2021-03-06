using System.ComponentModel.DataAnnotations;
using Application.Common.Interfaces.Repository;
using AutoMapper;
using Domain.Entities.Projects;
using FluentResults;
using MediatR;

namespace Application.Projects.Commands;

public class AddEditProjectCommand : IRequest<Result<string>>
{
    public int Id { get; set; } = 0;
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string ProjectManagerId { get; set; }
    [Required]
    public DateTime? StartDate { get; set; }
    public DateTime? DueDate { get; set; }
    public List<string> UserIds { get; set; } = new List<string>();
}

internal class AddEditProjectCommandHandler : IRequestHandler<AddEditProjectCommand, Result<string>>
{
    private readonly IRepository<Project, int> _repository;
    private readonly IMapper _mapper;
    public AddEditProjectCommandHandler(IRepository<Project, int> repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }
    public async Task<Result<string>> Handle(AddEditProjectCommand request, CancellationToken cancellationToken)
    {
        if (request.Id == 0)
        {
            var project = _mapper.Map<Project>(request);
            await _repository.AddAsync(project);
            await _repository.Save();
            return Result.Ok("Created new project!");
        }
        else
        {
            var project = await _repository.GetByIdAsync(request.Id);
            project.Name = request.Name;
            project.Description = request.Description;
            project.StartDate = request.StartDate ?? DateTime.MinValue;
            project.ProjectManagerId = request.ProjectManagerId;
            project.DueDate = request.DueDate ?? DateTime.MinValue;
            project.UserIds = request.UserIds.SequenceEqual(project.UserIds) ? project.UserIds : request.UserIds;
            await _repository.UpdateAsync(project);
            await _repository.Save();
            return Result.Ok("updated project!");
        }
    }
}