using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Sectors.Commands.CreateSector;

public record CreateSectorCommand : IRequest<int>
{
    public string? Name { get; set; }
    public int VerticalId { get; set; }
}

public class CreateSectorCommandHandler : IRequestHandler<CreateSectorCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateSectorCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateSectorCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Sectors
                    .Where(s => s.Name == request.Name)
                    .SingleOrDefaultAsync(cancellationToken);
        if (entity != null)
        {
            throw new ArgumentException("Sector name already exists.");
        }

        var sector = new Sector(request.Name, request.VerticalId);

        _context.Sectors.Add(sector);
        await _context.SaveChangesAsync(cancellationToken);

        return sector.Id;
    }

}