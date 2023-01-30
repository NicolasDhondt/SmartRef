using FluentValidation;

namespace SmartRef.Application.Tags.Commands.CreateProjectTag;

public class CreateProjectTagCommandValidator : AbstractValidator<CreateProjectTagCommand>
{

    public CreateProjectTagCommandValidator()
    {
        RuleFor(pt => pt.ProjectId).GreaterThan(0);
        RuleFor(pt => pt.TagId).GreaterThan(0);
    }

}