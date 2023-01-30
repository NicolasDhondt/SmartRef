using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRef.Application.Common.DTOs;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Contacts.Queries;

public record GetContactsQuery : IRequest<IEnumerable<ContactDTO>>;

public class GetContactsQueryHandler : IRequestHandler<GetContactsQuery, IEnumerable<ContactDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetContactsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ContactDTO>> Handle(GetContactsQuery request, CancellationToken cancellationToken)
    {
        List<Contact> contacts = await _context.Contacts
            .OrderBy(c => c.Name)
            .ToListAsync(cancellationToken);
        return _mapper.Map<List<ContactDTO>>(contacts);
    }
}