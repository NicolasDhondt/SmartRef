using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRef.Application.Common.DTOs;
using SmartRef.Application.Common.Exceptions;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Projects.Queries;

public record GetProjectQuery(int Id) : IRequest<ProjectDTO>;

public class GetProjectQueryHandler : IRequestHandler<GetProjectQuery, ProjectDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProjectQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ProjectDTO> Handle(GetProjectQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Projects
            .Where(p => p.Id == request.Id)
            .Include(p => p.Customer).Include(p => p.Customer.Vertical).Include(p => p.Customer.Sector)
            .Include(p => p.ProjectTechnologies).ThenInclude(pt => pt.Technology)
            .Include(p => p.ProjectTribeOffers).ThenInclude(pto => pto.TribeOffer)
            .Include(p => p.ProjectCountries).ThenInclude(pc => pc.Country)
            .Include(p => p.ProjectContacts).ThenInclude(pc => pc.Contact)
            .Include(p => p.ProjectTargetsWork).ThenInclude(ptw => ptw.TargetWork)
            .SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(Project), request.Id);

        return _mapper.Map<ProjectDTO>(entity);
    }
}