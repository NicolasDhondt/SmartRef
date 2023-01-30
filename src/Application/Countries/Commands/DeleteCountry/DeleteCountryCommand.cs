using MediatR;
using SmartRef.Application.Common.Exceptions;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Countries.Commands.DeleteCountry;
public record DeleteCountryCommand(int Id) : IRequest;

public class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteCountryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Countries
            .FindAsync(new object[] { request.Id }, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Country), request.Id);
        }

        _context.Countries.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

}