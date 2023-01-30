using MediatR;
using SmartRef.Application.Common.Exceptions;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Verticals.Commands.DeleteVertical;   

public record DeleteVerticalCommand(int Id) : IRequest;

public class DeleteVerticalCommandHandler : IRequestHandler<DeleteVerticalCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteVerticalCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteVerticalCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Verticals
            .FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Vertical), request.Id);
        }

        _context.Verticals.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

}