using FluentValidation;

namespace SmartRef.Application.Projects.Commands.UpdateProject;

public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
{
    public UpdateProjectCommandValidator()
    {
        RuleFor(p => p.Name).MaximumLength(200).NotEmpty();
        RuleFor(p => p.EndYear).GreaterThan(1950);
        RuleFor(p => p.Solutions).MaximumLength(380).NotEmpty();
        RuleFor(p => p.Benefits).MaximumLength(380).NotEmpty();
        RuleFor(p => p.Issues).MaximumLength(380).NotEmpty();
        RuleFor(p => p.Price).GreaterThan(0);
        RuleFor(p => p.ManDay).GreaterThan(0);
        RuleFor(p => p.CustomerId).GreaterThan(0);
    }
}