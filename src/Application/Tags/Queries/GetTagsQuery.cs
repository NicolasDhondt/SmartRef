using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRef.Application.Common.DTOs;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Tags.Queries;
public record GetTagsQuery : IRequest<IEnumerable<TagDTO>>;

public class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, IEnumerable<TagDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTagsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TagDTO>> Handle(GetTagsQuery request, CancellationToken cancellationToken)
    {
        List<Tag> tags = await _context.Tags
        .OrderBy(t => t.Name)
        .ToListAsync(cancellationToken);
        return _mapper.Map<List<TagDTO>>(tags);
    }
}