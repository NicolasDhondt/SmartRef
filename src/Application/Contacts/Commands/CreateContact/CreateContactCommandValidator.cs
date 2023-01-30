using FluentValidation;

namespace SmartRef.Application.Contacts.Commands.CreateContact;

public class CreateContactCommandValidator : AbstractValidator<CreateContactCommand>
{

    public CreateContactCommandValidator()
    {
        RuleFor(c => c.Name).MaximumLength(150).NotEmpty();
    }

}