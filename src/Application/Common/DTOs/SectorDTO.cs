using AutoMapper;
using SmartRef.Application.Common.Mappings;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Common.DTOs;

public class SectorDTO : IMapFrom<Sector>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int VerticalId { get; set; }
    public Vertical? Vertical { get; set; }
    public List<Customer> Customers { get; set; } = new List<Customer>();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Sector, SectorDTO>();
        profile.CreateMap<SectorDTO, Sector>();
    }

}
