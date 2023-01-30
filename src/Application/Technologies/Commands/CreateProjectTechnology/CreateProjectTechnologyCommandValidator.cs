using FluentValidation;

namespace SmartRef.Application.Technologies.Commands.CreateProjectTechnology;

public class CreateProjectTechnologyCommandValidator : AbstractValidator<CreateProjectTechnologyCommand>
{

    public CreateProjectTechnologyCommandValidator()
    {
        RuleFor(pt => pt.ProjectId).GreaterThan(0);
        RuleFor(pt => pt.TechnologyId).GreaterThan(0);
    }

}