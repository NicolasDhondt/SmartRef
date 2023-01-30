using FluentValidation;

namespace SmartRef.Application.Sectors.Commands.CreateSector;

public class CreateSectorCommandValidator : AbstractValidator<CreateSectorCommand>
{
    public CreateSectorCommandValidator()
    {
        RuleFor(s => s.Name).MaximumLength(120).NotEmpty();
        RuleFor(s => s.VerticalId).GreaterThan(0);
    }
}