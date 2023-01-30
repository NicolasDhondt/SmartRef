using AutoMapper;
using SmartRef.Application.Common.Mappings;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Common.DTOs;

public class CustomerDTO : IMapFrom<Customer>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string PhotoUrl { get; set; } = string.Empty;
    public int VerticalId { get; set; }
    public Vertical? Vertical { get; set; }
    public int SectorId { get; set; }
    public Sector? Sector { get; set; }
    public Project? Project { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Customer, CustomerDTO>();
        profile.CreateMap<CustomerDTO, Customer>();
    }

}
