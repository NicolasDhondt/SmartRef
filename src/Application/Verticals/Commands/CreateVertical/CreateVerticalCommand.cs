using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Verticals.Commands.CreateVertical;

public class CreateVerticalCommand : IRequest<int>
{
    public string? Name { get; set; }
}

public class CreateVerticalCommandHandler : IRequestHandler<CreateVerticalCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateVerticalCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateVerticalCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Verticals
                    .Where(v => v.Name == request.Name)
                    .SingleOrDefaultAsync(cancellationToken);
        if (entity != null)
        {
            throw new ArgumentException("Vertical name already exists.");
        }

        var vertical = new Vertical(request.Name);

        _context.Verticals.Add(vertical);
        await _context.SaveChangesAsync(cancellationToken);

        return vertical.Id;
    }

}