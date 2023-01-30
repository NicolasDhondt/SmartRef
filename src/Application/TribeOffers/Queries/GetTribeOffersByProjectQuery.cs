using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRef.Application.Common.DTOs;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.TribeOffers.Queries;
public record GetTribeOffersByProjectQuery(int ProjectId) : IRequest<IEnumerable<TribeOfferDTO>>;

public class GetTribeOffersByProjectQueryHandler : IRequestHandler<GetTribeOffersByProjectQuery, IEnumerable<TribeOfferDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTribeOffersByProjectQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TribeOfferDTO>> Handle(GetTribeOffersByProjectQuery request, CancellationToken cancellationToken)
    {
        List<TribeOffer?> tribeOffers = await _context.ProjectTribeOffers
            .Where(pto => pto.ProjectId == request.ProjectId)
            .Select(pto => pto.TribeOffer)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<TribeOfferDTO>>(tribeOffers);
    }

}