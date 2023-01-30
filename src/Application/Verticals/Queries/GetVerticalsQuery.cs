using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRef.Application.Common.DTOs;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Verticals.Queries;

public record GetVerticalsQuery : IRequest<IEnumerable<VerticalDTO>>;

public class GetVerticalsQueryHandler : IRequestHandler<GetVerticalsQuery, IEnumerable<VerticalDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetVerticalsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<VerticalDTO>> Handle(GetVerticalsQuery request, CancellationToken cancellationToken)
    {
        List<Vertical> verticals = await _context.Verticals
            .OrderBy(v => v.Name)
            .ToListAsync(cancellationToken);
        return _mapper.Map<List<VerticalDTO>>(verticals);
    }
}