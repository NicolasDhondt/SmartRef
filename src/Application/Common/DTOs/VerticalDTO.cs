using AutoMapper;
using SmartRef.Application.Common.Mappings;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Common.DTOs;
public class VerticalDTO : IMapFrom<Vertical>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Sector> Sectors { get; set; } = new List<Sector>();
    public List<Customer> Customers { get; set; } = new List<Customer>();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Vertical, VerticalDTO>();
        profile.CreateMap<VerticalDTO, Vertical>();
    }

}
