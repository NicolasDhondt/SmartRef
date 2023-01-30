using MediatR;
using SmartRef.Application.Common.Exceptions;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Channels.Commands.DeleteChannel;

public record DeleteChannelCommand(int Id) : IRequest;

public class DeleteChannelCommandHandler : IRequestHandler<DeleteChannelCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteChannelCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteChannelCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Channels
            .FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Channel), request.Id);
        }

        _context.Channels.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

}