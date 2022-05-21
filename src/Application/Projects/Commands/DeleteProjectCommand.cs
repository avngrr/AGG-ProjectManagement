using Application.Common.Interfaces.Repository;
using Domain.Entities.Projects;
using MediatR;

namespace Application.Projects.Commands;

public class DeleteProjectCommand : IRequest<int>
{
    public int Id { get; set; }
}

internal class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, int>
{
    private readonly IRepository<Project, int> _repository;
    public DeleteProjectCommandHandler(IRepository<Project, int> repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetByIdAsync(request.Id);
        await _repository.DeleteAsync(project);
        return 0;
    }
}