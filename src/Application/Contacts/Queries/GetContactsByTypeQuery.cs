using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRef.Application.Common.DTOs;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;
using SmartRef.Domain.Enums;

namespace SmartRef.Application.Contacts.Queries;

public record GetContactsByTypeQuery(ContactType Type) : IRequest<IEnumerable<ContactDTO>>;

public class GetContactsByTypeQueryHandler : IRequestHandler<GetContactsByTypeQuery, IEnumerable<ContactDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetContactsByTypeQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ContactDTO>> Handle(GetContactsByTypeQuery request, CancellationToken cancellationToken)
    {
        List<Contact?> contacts = await _context.ProjectContacts
            .Where(pc => pc.ContactType == request.Type)
            .Select(pc => pc.Contact)
            .Distinct() // because a contact can be in sales and in delivery at the same times
            .OrderBy(c => c.Name)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<ContactDTO>>(contacts);
    }
}