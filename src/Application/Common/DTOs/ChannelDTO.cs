using AutoMapper;
using SmartRef.Application.Common.Mappings;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Common.DTOs;
public class ChannelDTO : IMapFrom<Channel>
{

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<ProjectChannels> ProjectChannels { get; set; } = new List<ProjectChannels>();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Channel, ChannelDTO>();
        profile.CreateMap<ChannelDTO, Channel>();
    }

}
