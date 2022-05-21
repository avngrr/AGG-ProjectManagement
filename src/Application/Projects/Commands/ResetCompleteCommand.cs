using Application.Common.Interfaces.Repository;
using Domain.Entities.Projects;
using MediatR;

namespace Application.Projects.Commands;

public class ResetCompleteCommand : IRequest<int>
{
    public int Id { get; set; }
}
internal class ResetCompleteCommandHandler : IRequestHandler<ResetCompleteCommand, int>
{
    private readonly IRepository<Project, int> _repository;

    public ResetCompleteCommandHandler(IRepository<Project, int> repository)
    {
        _repository = repository;
    }
    public async Task<int> Handle(ResetCompleteCommand request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetByIdAsync(request.Id);
        project.CompleteData = null;
        await _repository.UpdateAsync(project);
        await _repository.Save();
        return 0;
    }
}