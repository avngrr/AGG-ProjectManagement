using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using Application.Common.Interfaces.Repository;
using Domain.Entities.Projects;
using FluentResults;
using MediatR;

namespace Application.Projects.Commands;

public class CompleteProjectCommand : IRequest<Result<string>>
{
    [Required]
    public int Id { get; set; }
}

internal class CompleteProjectCommandHandler : IRequestHandler<CompleteProjectCommand, Result<string>>
{
    private readonly IRepository<Project, int> _repository;

    public CompleteProjectCommandHandler(IRepository<Project, int> repository)
    {
        _repository = repository;
    }
    public async Task<Result<string>> Handle(CompleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetByIdAsync(request.Id);
        if (project is null)
        {
            return Result.Fail("Project not found!");
        }
        project.CompleteData = DateTime.Now;
        await _repository.UpdateAsync(project);
        await _repository.Save();
        return Result.Ok("Project set to complete!");
    }
}