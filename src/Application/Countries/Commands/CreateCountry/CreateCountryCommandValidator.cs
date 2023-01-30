using FluentValidation;

namespace SmartRef.Application.Countries.Commands.CreateCountry;

public class CreateCountryCommandValidator : AbstractValidator<CreateCountryCommand>
{
    public CreateCountryCommandValidator()
    {
        RuleFor(a => a.Name).MaximumLength(120).NotEmpty();
        RuleFor(a => a.Initial).MaximumLength(10).NotEmpty();
    }
}