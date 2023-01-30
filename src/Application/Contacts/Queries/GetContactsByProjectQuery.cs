using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRef.Application.Common.DTOs;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Contacts.Queries;

public record GetContactsByProjectQuery(int ProjectId) : IRequest<IEnumerable<ContactDTO>>;

public class GetContactsByProjectQueryHandler : IRequestHandler<GetContactsByProjectQuery, IEnumerable<ContactDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetContactsByProjectQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ContactDTO>> Handle(GetContactsByProjectQuery request, CancellationToken cancellationToken)
    {
        List<Contact?> contacts = await _context.ProjectContacts
            .Where(pc => pc.ProjectId == request.ProjectId)
            .Select(pc => pc.Contact).OrderBy(c => c.Name)
            .Distinct() //because a contact can be in sales and in delivery at the same times
            .ToListAsync(cancellationToken); 

        return _mapper.Map<List<ContactDTO>>(contacts);
    }
}