using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Countries.Commands.CreateProjectCountry;

public record CreateProjectCountryCommand : IRequest<int>
{
    public int ProjectId { get; set; }
    public int CountryId { get; set; }
}

public class CreateProjectCountryCommandHandler : IRequestHandler<CreateProjectCountryCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateProjectCountryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateProjectCountryCommand request, CancellationToken cancellationToken)
    {
        var entity = new ProjectCountries(request.ProjectId, request.CountryId);

        var isProjectCountriesAlreadyExist = await _context.ProjectCountries.ContainsAsync(entity, cancellationToken);
        if (isProjectCountriesAlreadyExist)
            throw new ArgumentException("The project country link already exists.");

        _context.ProjectCountries.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.CountryId;
    }

}
