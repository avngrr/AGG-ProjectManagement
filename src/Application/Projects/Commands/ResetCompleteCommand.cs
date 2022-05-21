using System.ComponentModel.DataAnnotations;
using Application.Common.Interfaces.Repository;
using Domain.Entities.Projects;
using FluentResults;
using MediatR;

namespace Application.Projects.Commands;

public class ResetCompleteCommand : IRequest<Result<string>>
{
    [Required]
    public int Id { get; set; }
}
internal class ResetCompleteCommandHandler : IRequestHandler<ResetCompleteCommand, Result<string>>
{
    private readonly IRepository<Project, int> _repository;

    public ResetCompleteCommandHandler(IRepository<Project, int> repository)
    {
        _repository = repository;
    }
    public async Task<Result<string>> Handle(ResetCompleteCommand request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetByIdAsync(request.Id);
        if (project is null)
        {
            return Result.Fail("Project not found!");
        }
        project.CompleteData = null;
        await _repository.UpdateAsync(project);
        await _repository.Save();
        return Result.Ok("Returned completed project to incomplete!");
    }
}