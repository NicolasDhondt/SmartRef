using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.TargetsWork.Commands.CreateTargetWork;

public record CreateTargetWorkCommand : IRequest<int>
{
    public string? Name { get; set; }
}

public class CreateTargetWorkCommandHandler : IRequestHandler<CreateTargetWorkCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateTargetWorkCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateTargetWorkCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TargetsWork
                    .Where(tw => tw.Name == request.Name)
                    .SingleOrDefaultAsync(cancellationToken);
        if (entity != null)
        {
            throw new ArgumentException("Target Work name already exists.");
        }

        var targetWork = new TargetWork(request.Name);

        _context.TargetsWork.Add(new TargetWork(request.Name));
        await _context.SaveChangesAsync(cancellationToken);

        return targetWork.Id;
    }

}