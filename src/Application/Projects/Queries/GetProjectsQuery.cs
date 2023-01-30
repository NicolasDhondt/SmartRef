using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRef.Application.Common.DTOs;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Application.Common.Models;

namespace SmartRef.Application.Projects.Queries;

public record GetProjectsQuery : IRequest<PaginatedList<ProjectDTO>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SearchText { get; set; }
    public string? VerticalId { get; set; }
    public string? SectorId { get; set; }
    public string? CustomerId { get; set; }
    public string? TechnologyId { get; set; }
    public string? CountryId { get; set; }
    public string? OfferId { get; set; }
    public string? TargetId { get; set; }
    public Boolean isPrivate { get; set; }
    public Boolean isPublic { get; set; }
}

public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, PaginatedList<ProjectDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProjectsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ProjectDTO>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        var projects = _context.Projects
            .Include(p => p.Customer)
            .OrderBy(p => p.Name);

        // filters
        if(!String.IsNullOrEmpty(request.SearchText))
        {
            projects = (IOrderedQueryable<Domain.Entities.Project>) projects.Where(p => 
            p.Customer.Name.ToUpper().Contains(request.SearchText.ToUpper()));
        }
        if(!String.IsNullOrEmpty(request.VerticalId))
        {
            projects = (IOrderedQueryable<Domain.Entities.Project>) projects.Where(p => 
            p.Customer.VerticalId == int.Parse(request.VerticalId));
        }
        if(!String.IsNullOrEmpty(request.SectorId))
        {
            projects = (IOrderedQueryable<Domain.Entities.Project>) projects.Where(p => 
            p.Customer.SectorId == int.Parse(request.SectorId));
        }
        if(!String.IsNullOrEmpty(request.CustomerId))
        {
            projects = (IOrderedQueryable<Domain.Entities.Project>) projects.Where(p => 
            p.Customer.Id == int.Parse(request.CustomerId));
        }
        if(!String.IsNullOrEmpty(request.TechnologyId))
        {
            projects = (IOrderedQueryable<Domain.Entities.Project>) projects.Where(p => 
                p.ProjectTechnologies.Any(pt => pt.TechnologyId == int.Parse(request.TechnologyId)));
        }
        if(!String.IsNullOrEmpty(request.CountryId))
        {
            projects = (IOrderedQueryable<Domain.Entities.Project>) projects.Where(p => 
                p.ProjectCountries.Any(pc => pc.CountryId == int.Parse(request.CountryId)));
        }
        if(!String.IsNullOrEmpty(request.OfferId))
        {
            projects = (IOrderedQueryable<Domain.Entities.Project>) projects.Where(p => 
                p.ProjectTribeOffers.Any(pto => pto.TribeOfferId == int.Parse(request.OfferId)));
        }
        if(!String.IsNullOrEmpty(request.TargetId))
        {
            projects = (IOrderedQueryable<Domain.Entities.Project>) projects.Where(p => 
                p.ProjectTargetsWork.Any(pt => pt.TargetWorkId == int.Parse(request.TargetId)));
        }
        if(!request.isPrivate && request.isPublic){
            projects = (IOrderedQueryable<Domain.Entities.Project>) projects.Where(p => p.IsPublic);
        }else if(request.isPrivate && !request.isPublic){
            projects = (IOrderedQueryable<Domain.Entities.Project>) projects.Where(p => !p.IsPublic);
        }else if(!request.isPrivate && !request.isPublic){
            projects = (IOrderedQueryable<Domain.Entities.Project>) projects.Where(p => p.IsPublic && !p.IsPublic); // no elements
        }
        List<ProjectDTO> projectsDTO = new();
        var projectsDTOs = _mapper.ProjectTo<ProjectDTO>(projects, projectsDTO);
        return await PaginatedList<ProjectDTO>.CreateAsync(projectsDTOs, request.PageNumber, request.PageSize);
    }

}