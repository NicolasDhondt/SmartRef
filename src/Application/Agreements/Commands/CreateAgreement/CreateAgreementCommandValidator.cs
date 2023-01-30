using FluentValidation;

namespace SmartRef.Application.Agreements.Commands.CreateAgreement;

public class CreateAgreementCommandValidator : AbstractValidator<CreateAgreementCommand>
{
    public CreateAgreementCommandValidator()
    {
        RuleFor(a => a.Name).MaximumLength(120).NotEmpty();
        RuleFor(a => a.Path).NotEmpty();
        RuleFor(a => a.ProjectId).GreaterThan(0);
    }
}