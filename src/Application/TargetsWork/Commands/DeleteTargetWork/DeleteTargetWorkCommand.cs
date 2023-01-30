using MediatR;
using SmartRef.Application.Common.Exceptions;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.TargetsWork.Commands.DeleteTargetWork;
public record DeleteTargetWorkCommand(int Id) : IRequest;

public class DeleteTargetWorkCommandHandler : IRequestHandler<DeleteTargetWorkCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteTargetWorkCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteTargetWorkCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TargetsWork
            .FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(TargetWork), request.Id);
        }

        _context.TargetsWork.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

}