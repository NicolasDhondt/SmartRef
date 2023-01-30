using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Countries.Commands.CreateCountry;

public record CreateCountryCommand : IRequest<int>
{
    public string? Name { get; set; }
    public string? Initial { get; set; }
}

public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateCountryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Countries
                    .Where(c => c.Name == request.Name)
                    .SingleOrDefaultAsync(cancellationToken);
        if (entity != null)
        {
            throw new ArgumentException("Country name already exists.");
        }

        var country = new Country(request.Name, request.Initial);

        _context.Countries.Add(country);
        await _context.SaveChangesAsync(cancellationToken);

        return country.Id;
    }

}