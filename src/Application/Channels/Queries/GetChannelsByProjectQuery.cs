using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRef.Application.Common.DTOs;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Channels.Queries;

public record GetChannelsByProjectQuery(int ProjectId) : IRequest<IEnumerable<ChannelDTO>>;

public class GetChannelsByProjectQueryHandler : IRequestHandler<GetChannelsByProjectQuery, IEnumerable<ChannelDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetChannelsByProjectQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ChannelDTO>> Handle(GetChannelsByProjectQuery request, CancellationToken cancellationToken)
    {
        List<Channel?> channels = await _context.ProjectChannels
            .Where(pc => pc.ProjectId == request.ProjectId)
            .Select(pc => pc.Channel)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<ChannelDTO>>(channels);
    }

}