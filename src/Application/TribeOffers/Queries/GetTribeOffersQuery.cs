using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRef.Application.Common.DTOs;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.TribeOffers.Queries;

public record GetTribeOffersQuery : IRequest<IEnumerable<TribeOfferDTO>>;

public class GetTribeOffersQueryHandler : IRequestHandler<GetTribeOffersQuery, IEnumerable<TribeOfferDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTribeOffersQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TribeOfferDTO>> Handle(GetTribeOffersQuery request, CancellationToken cancellationToken)
    {
        List<TribeOffer> tribeOffers = await _context.TribeOffers
        .OrderBy(to => to.Id)
        .ToListAsync(cancellationToken);
        return _mapper.Map<List<TribeOfferDTO>>(tribeOffers);
    }
}