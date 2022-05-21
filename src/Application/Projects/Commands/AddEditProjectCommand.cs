using MediatR;

namespace Application.Projects.Commands;

public class AddEditProjectCommand : IRequest<int>
{
    
}

internal class AddEditProjectCommandHandler : IRequestHandler<AddEditProjectCommand, int>
{
    public Task<int> Handle(AddEditProjectCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}