using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Channels.Commands.CreateProjectChannel;

public record CreateProjectChannelCommand : IRequest<int>
{
    public int ProjectId { get; set; }
    public int ChannelId { get; set; }
}

public class CreateProjectChannelCommandHandler : IRequestHandler<CreateProjectChannelCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateProjectChannelCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateProjectChannelCommand request, CancellationToken cancellationToken)
    {
        var entity = new ProjectChannels
        {
            ProjectId = request.ProjectId,
            ChannelId = request.ChannelId
        };

        var isProjectChannelsAlreadyExist = await _context.ProjectChannels.ContainsAsync(entity, cancellationToken);
        if (isProjectChannelsAlreadyExist)
            throw new ArgumentException("The project channel link already exists.");

        _context.ProjectChannels.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.ChannelId;
    }

}
