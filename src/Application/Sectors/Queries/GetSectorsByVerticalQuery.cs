using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRef.Application.Common.DTOs;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Sectors.Queries;

public record GetSectorsByVerticalQuery(int VerticalId) : IRequest<IEnumerable<SectorDTO>>;

public class GetSectorsByVerticalQueryHandler : IRequestHandler<GetSectorsByVerticalQuery, IEnumerable<SectorDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetSectorsByVerticalQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SectorDTO>> Handle(GetSectorsByVerticalQuery request, CancellationToken cancellationToken)
    {
        List<Sector> sectors = await _context.Sectors
            .Where(s => s.VerticalId == request.VerticalId)
            .OrderBy(s => s.Name)
            .ToListAsync(cancellationToken);
        return _mapper.Map<List<SectorDTO>>(sectors);
    }

}