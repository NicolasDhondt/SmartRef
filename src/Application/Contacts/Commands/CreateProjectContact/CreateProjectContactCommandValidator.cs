using FluentValidation;

namespace SmartRef.Application.Contacts.Commands.CreateProjectContact;

public class CreateProjectContactCommandValidator : AbstractValidator<CreateProjectContactCommand>
{

    public CreateProjectContactCommandValidator()
    {
        RuleFor(pc => pc.ProjectId).GreaterThan(0);
        RuleFor(pc => pc.ContactId).GreaterThan(0);
        RuleFor(pc => pc.ContactType).IsInEnum();
    }

}