using MediatR;
using SmartRef.Application.Common.Exceptions;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Technologies.Commands.DeleteTechnology;

public record DeleteTechnologyCommand(int Id) : IRequest;

public class DeleteTechnologyCommandHandler : IRequestHandler<DeleteTechnologyCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteTechnologyCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteTechnologyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Technologies
            .FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Technology), request.Id);
        }

        _context.Technologies.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

}