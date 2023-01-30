using AutoMapper;
using SmartRef.Application.Common.Mappings;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Common.DTOs;

public class ContactDTO : IMapFrom<Contact>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<ProjectContacts> ProjectContacts { get; set; } = new List<ProjectContacts>();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Contact, ContactDTO>();
        profile.CreateMap<ContactDTO, Contact>();
    }

}
