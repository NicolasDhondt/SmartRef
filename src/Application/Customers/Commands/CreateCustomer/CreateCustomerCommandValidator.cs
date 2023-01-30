using FluentValidation;

namespace SmartRef.Application.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(c => c.Name).MaximumLength(100).NotEmpty();
        RuleFor(c => c.VerticalId).GreaterThan(0);
        RuleFor(c => c.SectorId).GreaterThan(0);
    }
}