using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRef.Application.Common.Exceptions;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommand : IRequest
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? PhotoUrl { get; set; } = null;
    public int VerticalId { get; set; }
    public int SectorId { get; set; }
}
public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCustomerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Customers
            .Where(p => p.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Customer), request.Id);
        }

        var customerNameAlreadyExist = await _context.Customers
            .Where(c => c.Id != request.Id)
            .Where(c => c.Name == request.Name)
            .SingleOrDefaultAsync(cancellationToken) != null;
        if (customerNameAlreadyExist)
        {
            throw new ArgumentException("Customer name already exists.");
        }

        entity.Name = request.Name;
        entity.PhotoUrl = request.PhotoUrl;
        entity.VerticalId = request.VerticalId;
        entity.SectorId = request.SectorId;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}