using AutoMapper;
using SmartRef.Application.Common.Mappings;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Common.DTOs;

public class CountryDTO : IMapFrom<Country>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Initial { get; set; } = string.Empty;
    public List<ProjectCountries> ProjectCountries { get; set; } = new List<ProjectCountries>();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Country, CountryDTO>();
        profile.CreateMap<CountryDTO, Country>();
    }

}
