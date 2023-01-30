using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;
using SmartRef.Domain.Enums;

namespace SmartRef.Application.Contacts.Commands.CreateProjectContact;

public record CreateProjectContactCommand : IRequest<int>
{
    public int ProjectId { get; set; }
    public int ContactId { get; set; }
    public ContactType ContactType { get; set; }
}

public class CreateProjectContactCommandHandler : IRequestHandler<CreateProjectContactCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateProjectContactCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateProjectContactCommand request, CancellationToken cancellationToken)
    {
        var entity = new ProjectContacts(request.ProjectId, request.ContactId, request.ContactType);

        var isProjectContactsAlreadyExist = await _context.ProjectContacts.ContainsAsync(entity, cancellationToken);
        if (isProjectContactsAlreadyExist)
            throw new ArgumentException("The project contact link already exists.");

        _context.ProjectContacts.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.ContactId;
    }

}
