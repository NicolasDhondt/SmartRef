using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Tags.Commands.CreateProjectTag;

public record CreateProjectTagCommand : IRequest<int>
{
    public int ProjectId { get; set; }
    public int TagId { get; set; }
}

public class CreateProjectTagCommandHandler : IRequestHandler<CreateProjectTagCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateProjectTagCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateProjectTagCommand request, CancellationToken cancellationToken)
    {
        var entity = new ProjectTags
        {
            ProjectId = request.ProjectId,
            TagId = request.TagId
        };

        var isProjectTagsAlreadyExist = await _context.ProjectTags.ContainsAsync(entity, cancellationToken);
        if (isProjectTagsAlreadyExist)
            throw new ArgumentException("The project tag link already exists.");

        _context.ProjectTags.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.TagId;
    }

}
