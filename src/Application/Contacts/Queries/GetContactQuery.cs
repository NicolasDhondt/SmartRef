using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRef.Application.Common.DTOs;
using SmartRef.Application.Common.Exceptions;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Contacts.Queries;

public record GetContactQuery(int Id) : IRequest<ContactDTO>;

public class GetContacQueryHandler : IRequestHandler<GetContactQuery, ContactDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetContacQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ContactDTO> Handle(GetContactQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Contacts
            .Where(c => c.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(Contact), request.Id);

        return _mapper.Map<ContactDTO>(entity);
    }
}