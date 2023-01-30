using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRef.Application.Common.DTOs;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Countries.Queries;
public record GetCountriesByProjectQuery(int ProjectId) : IRequest<IEnumerable<CountryDTO>>;

public class GetCountriesByProjectHandler : IRequestHandler<GetCountriesByProjectQuery, IEnumerable<CountryDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCountriesByProjectHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CountryDTO>> Handle(GetCountriesByProjectQuery request, CancellationToken cancellationToken)
    {
        List<Country?> countries = await _context.ProjectCountries
            .Where(pc => pc.ProjectId == request.ProjectId)
            .Select(pc => pc.Country)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<CountryDTO>>(countries);
    }

}