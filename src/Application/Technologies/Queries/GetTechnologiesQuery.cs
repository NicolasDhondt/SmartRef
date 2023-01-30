using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRef.Application.Common.DTOs;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Technologies.Queries;
public record GetTechnologiesQuery : IRequest<IEnumerable<TechnologyDTO>>;

public class GetTechnologiesQueryHandler : IRequestHandler<GetTechnologiesQuery, IEnumerable<TechnologyDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTechnologiesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TechnologyDTO>> Handle(GetTechnologiesQuery request, CancellationToken cancellationToken)
    {
        List<Technology> technologies = await _context.Technologies
            .OrderBy(t => t.Name)
            .ToListAsync(cancellationToken);
        return _mapper.Map<List<TechnologyDTO>>(technologies);
    }
}