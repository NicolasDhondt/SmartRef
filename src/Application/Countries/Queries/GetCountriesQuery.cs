using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRef.Application.Common.DTOs;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Countries.Queries;

public record GetCountriesQuery : IRequest<IEnumerable<CountryDTO>>;

public class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, IEnumerable<CountryDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCountriesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CountryDTO>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
    {
        List<Country> countries = await _context.Countries
            .OrderBy(c => c.Name)
            .ToListAsync(cancellationToken);
        return _mapper.Map<List<CountryDTO>>(countries);
    }
}