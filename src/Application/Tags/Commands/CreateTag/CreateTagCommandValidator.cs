using FluentValidation;

namespace SmartRef.Application.Tags.Commands.CreateTag;

public class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
{
    public CreateTagCommandValidator()
    {
        RuleFor(a => a.Name).MaximumLength(120).NotEmpty();
    }
}