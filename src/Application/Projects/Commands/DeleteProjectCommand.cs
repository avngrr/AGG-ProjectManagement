using System.ComponentModel.DataAnnotations;
using Application.Common.Interfaces.Repository;
using Domain.Entities.Projects;
using FluentResults;
using MediatR;

namespace Application.Projects.Commands;

public class DeleteProjectCommand : IRequest<Result<string>>
{
    [Required]
    public int Id { get; set; }
}

internal class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, Result<string>>
{
    private readonly IRepository<Project, int> _repository;
    public DeleteProjectCommandHandler(IRepository<Project, int> repository)
    {
        _repository = repository;
    }

    public async Task<Result<string>> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetByIdAsync(request.Id);
        if (project is null)
        {
            return Result.Fail("Project not found!");
        }
        await _repository.DeleteAsync(project);
        await _repository.Save();
        return Result.Ok("Deleted project!");
    }
}