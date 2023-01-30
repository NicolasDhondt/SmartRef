using FluentValidation;

namespace SmartRef.Application.Countries.Commands.CreateProjectCountry;

public class CreateProjectCountryCommandValidator : AbstractValidator<CreateProjectCountryCommand>
{

    public CreateProjectCountryCommandValidator()
    {
        RuleFor(pc => pc.ProjectId).GreaterThan(0);
        RuleFor(pc => pc.CountryId).GreaterThan(0);
    }

}