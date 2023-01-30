using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRef.Application.Common.DTOs;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.TargetsWork.Queries;
public record GetTargetsWorkByProjectQuery(int ProjectId) : IRequest<IEnumerable<TargetWorkDTO>>;

public class GetTargetsWorkByProjectQueryHandler : IRequestHandler<GetTargetsWorkByProjectQuery, IEnumerable<TargetWorkDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTargetsWorkByProjectQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TargetWorkDTO>> Handle(GetTargetsWorkByProjectQuery request, CancellationToken cancellationToken)
    {
        List<TargetWork?> targetsWork = await _context.ProjectTargetsWork
            .Where(ptw => ptw.ProjectId == request.ProjectId)
            .Select(ptw => ptw.TargetWork)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<TargetWorkDTO>>(targetsWork);
    }

}