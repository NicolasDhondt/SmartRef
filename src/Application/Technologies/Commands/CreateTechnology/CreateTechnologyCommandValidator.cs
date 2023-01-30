using FluentValidation;

namespace SmartRef.Application.Technologies.Commands.CreateTechnology;

public class CreateTechnologyCommandValidator : AbstractValidator<CreateTechnologyCommand>
{
    public CreateTechnologyCommandValidator()
    {
        RuleFor(a => a.Name).MaximumLength(120).NotEmpty();
    }
}
