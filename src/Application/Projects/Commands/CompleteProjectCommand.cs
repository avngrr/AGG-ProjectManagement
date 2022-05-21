using System.Security.Cryptography;
using Application.Common.Interfaces.Repository;
using Domain.Entities.Projects;
using MediatR;

namespace Application.Projects.Commands;

public class CompleteProjectCommand : IRequest<int>
{
    public int Id { get; set; }
}

internal class CompleteProjectCommandHandler : IRequestHandler<CompleteProjectCommand, int>
{
    private readonly IRepository<Project, int> _repository;

    public CompleteProjectCommandHandler(IRepository<Project, int> repository)
    {
        _repository = repository;
    }
    public async Task<int> Handle(CompleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetByIdAsync(request.Id);
        project.CompleteData = DateTime.Now;
        await _repository.UpdateAsync(project);
        await _repository.Save();
        return 0;
    }
}