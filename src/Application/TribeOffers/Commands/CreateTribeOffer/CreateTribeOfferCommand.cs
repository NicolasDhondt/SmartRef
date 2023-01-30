using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.TribeOffers.Commands.CreateTribeOffer;

public record CreateTribeOfferCommand : IRequest<int>
{
    public string? Name { get; set; }
}

public class CreateTribeOfferCommandHandler : IRequestHandler<CreateTribeOfferCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateTribeOfferCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateTribeOfferCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TribeOffers
                    .Where(to => to.Name == request.Name)
                    .SingleOrDefaultAsync(cancellationToken);
        if (entity != null)
        {
            throw new ArgumentException("Tribe Offer name already exists.");
        }

        var tribeOffer = new TribeOffer(request.Name);

        _context.TribeOffers.Add(tribeOffer);
        await _context.SaveChangesAsync(cancellationToken);

        return tribeOffer.Id;
    }

}