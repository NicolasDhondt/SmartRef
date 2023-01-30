using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Agreements.Commands.CreateAgreement;
public record CreateAgreementCommand : IRequest<int>
{
    public string? Name { get; set; }
    public string? Path { get; set; } // = default path?
    public int ProjectId { get; set; }
}

public class CreateAgreementCommandHandler : IRequestHandler<CreateAgreementCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateAgreementCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateAgreementCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Agreements
            .Where(a => a.Name == request.Name)
            .SingleOrDefaultAsync(cancellationToken);
        if (entity != null)
        {
            throw new ArgumentException("Agreement name already exists.");
        }

        var agreement = new Agreement
        {
            Name = request.Name,
            Path = request.Path,
            ProjectId = request.ProjectId,
        };

        _context.Agreements.Add(agreement);
        await _context.SaveChangesAsync(cancellationToken);

        return agreement.Id;
    }

}
