using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRef.Application.Common.DTOs;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.TargetsWork.Queries;
public record GetTargetsWorkQuery : IRequest<IEnumerable<TargetWorkDTO>>;

public class GetTargetsWorkQueryHandler : IRequestHandler<GetTargetsWorkQuery, IEnumerable<TargetWorkDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTargetsWorkQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TargetWorkDTO>> Handle(GetTargetsWorkQuery request, CancellationToken cancellationToken)
    {
        List<TargetWork> targetsWork = await _context.TargetsWork.ToListAsync(cancellationToken);
        return _mapper.Map<List<TargetWorkDTO>>(targetsWork);
    }
}