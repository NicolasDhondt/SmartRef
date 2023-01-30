using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRef.Application.Common.DTOs;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Channels.Queries;

public record GetChannelsQuery : IRequest<IEnumerable<ChannelDTO>>;

public class GetChannelsQueryHandler : IRequestHandler<GetChannelsQuery, IEnumerable<ChannelDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetChannelsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ChannelDTO>> Handle(GetChannelsQuery request, CancellationToken cancellationToken)
    {
        List<Channel> channels = await _context.Channels.ToListAsync(cancellationToken);
        return _mapper.Map<List<ChannelDTO>>(channels);
    }
}