using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRef.Application.Common.DTOs;
using SmartRef.Application.Common.Exceptions;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Technologies.Queries;

public record GetTechnologyQuery(int Id) : IRequest<TechnologyDTO>;

public class GetTechnologyQueryHandler : IRequestHandler<GetTechnologyQuery, TechnologyDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTechnologyQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TechnologyDTO> Handle(GetTechnologyQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Technologies
            .Where(p => p.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(Technology), request.Id);

        return _mapper.Map<TechnologyDTO>(entity);
    }
}