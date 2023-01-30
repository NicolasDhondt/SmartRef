using MediatR;
using SmartRef.Application.Common.Exceptions;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Tags.Commands.DeleteTag;

public record DeleteTagCommand(int Id) : IRequest;

public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteTagCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Tags
            .FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Tag), request.Id);
        }

        _context.Tags.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

}