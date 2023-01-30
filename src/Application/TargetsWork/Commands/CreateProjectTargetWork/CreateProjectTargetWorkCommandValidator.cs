using FluentValidation;
using SmartRef.Application.TargetsWork.Commands.CreateProjectTargetsWork;

namespace SmartRef.Application.TargetsWork.Commands.CreateProjectTargetWork;
public class CreateProjectTargetWorkCommandValidator : AbstractValidator<CreateProjectTargetWorkCommand>
{

    public CreateProjectTargetWorkCommandValidator()
    {
        RuleFor(pt => pt.ProjectId).GreaterThan(0);
        RuleFor(pt => pt.TargetWorkId).GreaterThan(0);
    }

}