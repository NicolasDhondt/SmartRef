using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Technologies.Commands.CreateProjectTechnology;
public record class CreateProjectTechnologyCommand : IRequest<int>
{
    public int ProjectId { get; set; }
    public int TechnologyId { get; set; }
}

public class CreateProjectTechnologyCommandHandler : IRequestHandler<CreateProjectTechnologyCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateProjectTechnologyCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateProjectTechnologyCommand request, CancellationToken cancellationToken)
    {
        var entity = new ProjectTechnologies(request.ProjectId, request.TechnologyId);

        var isProjectTechnologiesAlreadyExist = await _context.ProjectTechnologies.ContainsAsync(entity, cancellationToken);
        if (isProjectTechnologiesAlreadyExist)
            throw new ArgumentException("The project technology link already exists.");

        _context.ProjectTechnologies.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.TechnologyId;
    }

}
