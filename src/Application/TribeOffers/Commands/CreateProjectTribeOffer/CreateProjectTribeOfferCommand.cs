using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.TribeOffers.Commands.CreateTribeOffer;
public record CreateProjectTribeOfferCommand : IRequest<int>
{
    public int ProjectId { get; set; }
    public int TribeOfferId { get; set; }
}

public class CreateProjectTribeOfferCommandHandler : IRequestHandler<CreateProjectTribeOfferCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateProjectTribeOfferCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateProjectTribeOfferCommand request, CancellationToken cancellationToken)
    {
        var entity = new ProjectTribeOffers(request.ProjectId, request.TribeOfferId);

        var isProjectProjectTribeOffersAlreadyExist = await _context.ProjectTribeOffers.ContainsAsync(entity, cancellationToken);
        if (isProjectProjectTribeOffersAlreadyExist)
            throw new ArgumentException("The project tribe offer link already exists.");

        _context.ProjectTribeOffers.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.TribeOfferId;
    }

}