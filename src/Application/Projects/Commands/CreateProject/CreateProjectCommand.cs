using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Projects.Commands.CreateProject;

public record CreateProjectCommand : IRequest<int>
{
    public string? Name { get; set; }
    public int EndYear { get; set; } = 2150;
    public bool IsFinished { get; set; }
    public decimal Price { get; set; }
    public int ManDay { get; set; }
    public string? Solutions { get; set; } = null;
    public string? Issues { get; set; } = null;
    public string? Benefits { get; set; } = null;
    public string? Narative { get; set; } = null;
    public bool IsPublic { get; set; }
    public string? PhotoUrl { get; set; } = null;
    public int CustomerId { get; set; }
}

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateProjectCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Projects
                    .Where(p => p.Name == request.Name)
                    .SingleOrDefaultAsync(cancellationToken);
        if (entity != null)
        {
            throw new ArgumentException("Project name already exists, you need to update it to make some changes.");
        }

        var project = new Project(
            request.Name,
            request.EndYear,
            request.IsFinished,
            request.Price,
            request.ManDay,
            request.Solutions,
            request.Issues,
            request.Benefits,
            request.Narative,
            request.IsPublic,
            request.PhotoUrl,
            request.CustomerId
        );
        
        _context.Projects.Add(project);
        await _context.SaveChangesAsync(cancellationToken);

        return project.Id;
    }

}