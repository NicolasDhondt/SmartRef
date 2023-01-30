using AutoMapper;
using SmartRef.Application.Common.Mappings;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Common.DTOs;
public class TechnologyDTO : IMapFrom<Technology>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<ProjectTechnologies> ProjectTechnologies { get; set; } = new List<ProjectTechnologies>();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Technology, TechnologyDTO>();
        profile.CreateMap<TechnologyDTO, Technology>();
    }

}