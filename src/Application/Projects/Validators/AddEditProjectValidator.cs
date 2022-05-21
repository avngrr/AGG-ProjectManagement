using Application.Projects.Commands;
using FluentValidation;

namespace Application.Projects.Validators;

public class AddEditProjectValidator : AbstractValidator<AddEditProjectCommand>
{
    public AddEditProjectValidator()
    {
        RuleFor(request => request.Name)
            .Must(s => !string.IsNullOrWhiteSpace(s)).WithMessage(s => "Name is required!");
        RuleFor(request => request.Description)
            .Must(s => !string.IsNullOrWhiteSpace(s)).WithMessage(s => "Description is required!");
        RuleFor(request => request.StartDate)
            .Must(d => d == DateTime.MinValue).WithMessage(s => "Startdate is required!");
        RuleFor(request => request.ProjectManagerId)
            .Must(s => !string.IsNullOrWhiteSpace(s)).WithMessage(s => "Project needs a projectmanager");
    }
}