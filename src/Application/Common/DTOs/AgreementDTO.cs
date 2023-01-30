using AutoMapper;
using SmartRef.Application.Common.Mappings;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Common.DTOs;

public class AgreementDTO : IMapFrom<Agreement>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public int ProjectId { get; set; }
    public Project? Project { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Agreement, AgreementDTO>();
        profile.CreateMap<AgreementDTO, Agreement>();
    }

}
