using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRef.Application.Common.Exceptions;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Projects.Commands.UpdateProject;
public record UpdateProjectCommand : IRequest
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int EndYear { get; set; } = 2150;
    public bool IsFinished { get; set; }
    public decimal Price { get; set; }
    public int ManDay { get; set; }
    public string? Solutions { get; set; }
    public string? Issues { get; set; }
    public string? Benefits { get; set; }
    public string? Narative { get; set; }
    public bool IsPublic { get; set; }
    public string? PhotoUrl { get; set; } = null;
    public int CustomerId { get; set; }
}
public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateProjectCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Projects
            .Where(p => p.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Project), request.Id);
        }

        var isNameAlreadyExist = await _context.Projects
                    .Where(p => p.Id != request.Id)
                    .Where(p => p.Name == request.Name)
                    .SingleOrDefaultAsync(cancellationToken) != null;
        if (isNameAlreadyExist)
        {
            throw new ArgumentException("Project name already exists, you need to update it to make some changes.");
        }

        entity.Name = request.Name;
        entity.EndYear = request.EndYear;
        entity.IsFinished = request.IsFinished;
        entity.Price = request.Price;
        entity.ManDay = request.ManDay;
        entity.Solutions = request.Solutions;
        entity.Issues = request.Issues;
        entity.Benefits = request.Benefits;
        entity.Narative = request.Narative;
        entity.IsPublic = request.IsPublic;
        entity.PhotoUrl = request.PhotoUrl;
        entity.CustomerId = request.CustomerId;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}