using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRef.Application.Common.DTOs;
using SmartRef.Application.Common.Exceptions;
using SmartRef.Application.Common.Interfaces;

namespace SmartRef.Application.Customers.Queries;

public record GetCustomerQuery(int Id) : IRequest<CustomerDTO>;

public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, CustomerDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCustomerQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CustomerDTO> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Customers
            .Where(c => c.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(entity), request.Id);

        return _mapper.Map<CustomerDTO>(entity);
    }
}