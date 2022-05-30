using System.ComponentModel.DataAnnotations;
using FluentResults;
using MediatR;

namespace Application.Utilities.Documents.Commands;

public class AddEditDocumentCommand : IRequest<Result<string>>
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public bool IsPublic { get; set; } = false;
    [Required]
    public int DocumentTypeId { get; set; }
}

internal class AddEditDocumentCommandHandler : IRequestHandler<AddEditDocumentCommand, Result<string>>
{
    public Task<Result<string>> Handle(AddEditDocumentCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}