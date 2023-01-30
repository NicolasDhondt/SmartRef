using FluentValidation;
using SmartRef.Application.TribeOffers.Commands.CreateTribeOffer;

namespace SmartRef.Application.TribeOffers.Commands.CreateProjectTribeOffer;
public class CreateProjectTribeOfferCommandValidator : AbstractValidator<CreateProjectTribeOfferCommand>
{

    public CreateProjectTribeOfferCommandValidator()
    {
        RuleFor(pt => pt.ProjectId).GreaterThan(0);
        RuleFor(pt => pt.TribeOfferId).GreaterThan(0);
    }

}