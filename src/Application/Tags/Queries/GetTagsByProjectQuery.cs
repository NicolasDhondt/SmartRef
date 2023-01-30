using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRef.Application.Common.DTOs;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Tags.Queries;
public record GetTagsByProjectQuery(int ProjectId) : IRequest<IEnumerable<TagDTO>>;

public class GetTagsByProjectQueryHandler : IRequestHandler<GetTagsByProjectQuery, IEnumerable<TagDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTagsByProjectQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TagDTO>> Handle(GetTagsByProjectQuery request, CancellationToken cancellationToken)
    {
        List<Tag?> tags = await _context.ProjectTags
            .Where(pt => pt.ProjectId == request.ProjectId)
            .Select(pt => pt.Tag)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<TagDTO>>(tags);
    }

}