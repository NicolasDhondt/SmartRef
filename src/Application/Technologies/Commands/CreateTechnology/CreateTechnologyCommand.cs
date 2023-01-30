using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Technologies.Commands.CreateTechnology;

public record CreateTechnologyCommand : IRequest<int>
{
    public string? Name { get; set; }
}

public class CreateTechnologyCommandHandler : IRequestHandler<CreateTechnologyCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateTechnologyCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateTechnologyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Technologies
                    .Where(t => t.Name == request.Name)
                    .SingleOrDefaultAsync(cancellationToken);
        if (entity != null)
        {
            throw new ArgumentException("Technology name already exists.");
        }

        var technology = new Technology(request.Name);

        _context.Technologies.Add(technology);
        await _context.SaveChangesAsync(cancellationToken);

        return technology.Id;
    }

}