using AutoMapper;
using SmartRef.Application.Common.Mappings;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Common.DTOs;
public class TribeOfferDTO : IMapFrom<TribeOffer>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<ProjectTribeOffers> ProjectTribeOffers { get; set; } = new List<ProjectTribeOffers>();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<TribeOffer, TribeOfferDTO>();
        profile.CreateMap<TribeOfferDTO, TribeOffer>();
    }

}