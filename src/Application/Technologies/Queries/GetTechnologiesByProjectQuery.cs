using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRef.Application.Common.DTOs;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Technologies.Queries;

public record GetTechnologiesByProjectQuery(int ProjectId) : IRequest<IEnumerable<TechnologyDTO>>;

public class GetTechnologiesByProjectQueryHandler : IRequestHandler<GetTechnologiesByProjectQuery, IEnumerable<TechnologyDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTechnologiesByProjectQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TechnologyDTO>> Handle(GetTechnologiesByProjectQuery request, CancellationToken cancellationToken)
    {
        List<Technology?> technologies = await _context.ProjectTechnologies
            .Where(pt => pt.ProjectId == request.ProjectId)
            .Select(pt => pt.Technology)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<TechnologyDTO>>(technologies);
    }

}