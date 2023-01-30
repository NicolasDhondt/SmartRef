﻿using AutoMapper;
using SmartRef.Application.Common.Mappings;
using SmartRef.Domain.Entities;

namespace SmartRef.Application.Common.DTOs;

public class ProjectDTO : IMapFrom<Project>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int EndYear { get; set; }
    public bool IsFinished { get; set; }
    public decimal Price { get; set; }
    public int ManDay { get; set; }
    public string? Solutions { get; set; }
    public string? Issues { get; set; }
    public string? Benefits { get; set; }
    public string? Narative { get; set; }
    public bool IsPublic { get; set; }
    public string? PhotoUrl { get; set; }
    public float DataQualityPercent { get; set; }
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public List<ProjectTribeOffers> ProjectTribeOffers { get; set; } = new List<ProjectTribeOffers>();
    public List<ProjectTags> ProjectTags { get; set; } = new List<ProjectTags>();
    public List<ProjectCountries> ProjectCountries { get; set; } = new List<ProjectCountries>();
    public List<ProjectTargetsWork> ProjectTargetsWork { get; set; } = new List<ProjectTargetsWork>();
    public List<ProjectContacts> ProjectContacts { get; set; } = new List<ProjectContacts>();
    public List<Agreement> Agreements { get; set; } = new List<Agreement>();
    public List<ProjectTechnologies> ProjectTechnologies { get; set; } = new List<ProjectTechnologies>();
    public List<ProjectChannels> ProjectChannels { get; set; } = new List<ProjectChannels>();

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Project, ProjectDTO>();
        profile.CreateMap<ProjectDTO, Project>();
    }

}