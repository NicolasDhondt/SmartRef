using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRef.Application.Common.DTOs;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Agreements.Queries;

public record GetAgreementsByProjectQuery(int ProjectId) : IRequest<IEnumerable<AgreementDTO>>;

public class GetAgreementsByProjectQueryHandler : IRequestHandler<GetAgreementsByProjectQuery, IEnumerable<AgreementDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAgreementsByProjectQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AgreementDTO>> Handle(GetAgreementsByProjectQuery request, CancellationToken cancellationToken)
    {
        List<Agreement> agreements = await _context.Agreements
            .Where(a => a.ProjectId == request.ProjectId)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<AgreementDTO>>(agreements);
    }

}