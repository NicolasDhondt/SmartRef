using MediatR;
using SmartRef.Application.Common.Exceptions;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.TribeOffers.Commands.DeleteTribeOffer;

public record DeleteTribeOfferCommand(int Id) : IRequest;

public class DeleteTribeOfferCommandHandler : IRequestHandler<DeleteTribeOfferCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteTribeOfferCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteTribeOfferCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TribeOffers
            .FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(TribeOffer), request.Id);
        }

        _context.TribeOffers.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

}