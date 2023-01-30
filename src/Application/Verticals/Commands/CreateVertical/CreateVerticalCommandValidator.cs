using FluentValidation;

namespace SmartRef.Application.Verticals.Commands.CreateVertical;
public class CreateVerticalCommandValidator : AbstractValidator<CreateVerticalCommand>
{

    public CreateVerticalCommandValidator()
    {
        RuleFor(a => a.Name).MaximumLength(120).NotEmpty();
    }

}