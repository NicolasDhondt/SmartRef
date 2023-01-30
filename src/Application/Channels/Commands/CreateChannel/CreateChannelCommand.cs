using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Channels.Commands.CreateChannel;

public record CreateChannelCommand : IRequest<int>
{
    public string? Name { get; set; }
}

public class CreateChannelCommandHandler : IRequestHandler<CreateChannelCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateChannelCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateChannelCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Channels
                    .Where(c => c.Name == request.Name)
                    .SingleOrDefaultAsync(cancellationToken);
        if (entity != null)
        {
            throw new ArgumentException("Channel name already exists.");
        }

        var channel = new Channel()
        {
            Name = request.Name
        };

        _context.Channels.Add(channel);
        await _context.SaveChangesAsync(cancellationToken);

        return channel.Id;
    }

}