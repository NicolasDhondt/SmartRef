using MediatR;
using SmartRef.Application.Common.Exceptions;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Sectors.Commands.DeleteSector;

public record DeleteSectorCommand(int Id) : IRequest;

public class DeleteSectorCommandHandler : IRequestHandler<DeleteSectorCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteSectorCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteSectorCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Sectors
            .FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Sector), request.Id);
        }

        _context.Sectors.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

}