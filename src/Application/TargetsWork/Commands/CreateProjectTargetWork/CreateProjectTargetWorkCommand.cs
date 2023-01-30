using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.TargetsWork.Commands.CreateProjectTargetsWork;
public record CreateProjectTargetWorkCommand : IRequest<int>
{
    public int ProjectId { get; set; }
    public int TargetWorkId { get; set; }
}

public class CreateProjectTargetWorkCommandHandler : IRequestHandler<CreateProjectTargetWorkCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateProjectTargetWorkCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateProjectTargetWorkCommand request, CancellationToken cancellationToken)
    {
        var entity = new ProjectTargetsWork(request.ProjectId, request.TargetWorkId);

        var isProjectTargetsWorkAlreadyExist = await _context.ProjectTargetsWork.ContainsAsync(entity, cancellationToken);
        if (isProjectTargetsWorkAlreadyExist)
            throw new ArgumentException("The project target work link already exists.");

        _context.ProjectTargetsWork.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.TargetWorkId;
    }

}
