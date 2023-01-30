using FluentValidation;

namespace SmartRef.Application.TribeOffers.Commands.CreateTribeOffer;

public class CreateTribeOfferCommandValidator : AbstractValidator<CreateTribeOfferCommand>
{
    public CreateTribeOfferCommandValidator()
    {
        RuleFor(a => a.Name).MaximumLength(120).NotEmpty();
    }
}
