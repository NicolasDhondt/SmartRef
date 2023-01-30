using FluentValidation;

namespace SmartRef.Application.TargetsWork.Commands.CreateTargetWork;
public class CreateTargetWorkCommandValidator : AbstractValidator<CreateTargetWorkCommand>
{
    public CreateTargetWorkCommandValidator()
    {
        RuleFor(a => a.Name).MaximumLength(120).NotEmpty();
    }
}