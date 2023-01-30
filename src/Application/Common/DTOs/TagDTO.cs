using AutoMapper;
using SmartRef.Application.Common.Mappings;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Common.DTOs;

public class TagDTO : IMapFrom<Tag>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<ProjectTags> ProjectTags { get; set; } = new List<ProjectTags>();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Tag, TagDTO>();
        profile.CreateMap<TagDTO, Tag>();
    }

}
