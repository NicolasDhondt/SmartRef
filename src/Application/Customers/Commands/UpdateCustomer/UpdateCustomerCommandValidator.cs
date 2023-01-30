using FluentValidation;

namespace SmartRef.Application.Customers.Commands.UpdateCustomer;
public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(c => c.Name).MaximumLength(100).NotEmpty();
        RuleFor(c => c.VerticalId).GreaterThan(0);
        RuleFor(c => c.SectorId).GreaterThan(0);
    }
}