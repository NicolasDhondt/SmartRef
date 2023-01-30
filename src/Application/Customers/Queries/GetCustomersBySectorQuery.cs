using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRef.Application.Common.DTOs;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace Application.Customers.Queries;

public record GetCustomersBySectorQuery(int SectorId) : IRequest<IEnumerable<CustomerDTO>>;

public class GetCustomersBySectorQueryHandler : IRequestHandler<GetCustomersBySectorQuery, IEnumerable<CustomerDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCustomersBySectorQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CustomerDTO>> Handle(GetCustomersBySectorQuery request, CancellationToken cancellationToken)
    {
        List<Customer> custommers = await _context.Customers
            .Where(c => c.SectorId == request.SectorId)
            .OrderBy(c => c.Name)
            .ToListAsync(cancellationToken);
        return _mapper.Map<List<CustomerDTO>>(custommers);
    }
}