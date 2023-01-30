using AutoMapper;
using SmartRef.Application.Common.Mappings;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Common.DTOs;
public class TargetWorkDTO : IMapFrom<TargetWork>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<ProjectTargetsWork> ProjectTargetsWork { get; set; } = new List<ProjectTargetsWork>();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<TargetWork, TargetWorkDTO>();
        profile.CreateMap<TargetWorkDTO, TargetWork>();
    }

}