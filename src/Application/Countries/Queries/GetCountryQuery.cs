using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRef.Application.Common.DTOs;
using SmartRef.Application.Common.Exceptions;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Countries.Queries;

public record GetCountryQuery(int Id) : IRequest<CountryDTO>;

public class GetCountryQueryHandler : IRequestHandler<GetCountryQuery, CountryDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCountryQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CountryDTO> Handle(GetCountryQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Countries
            .Where(p => p.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(Country), request.Id);

        return _mapper.Map<CountryDTO>(entity);
    }
}