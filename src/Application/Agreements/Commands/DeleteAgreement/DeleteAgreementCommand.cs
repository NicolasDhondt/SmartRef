using MediatR;
using SmartRef.Application.Common.Exceptions;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Agreements.Commands.DeleteAgreement;

public record DeleteAgreementCommand(int Id) : IRequest;

public class DeleteAgreemetCommandHandler : IRequestHandler<DeleteAgreementCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteAgreemetCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteAgreementCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Agreements
            .FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Agreement), request.Id);
        }

        _context.Agreements.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

}