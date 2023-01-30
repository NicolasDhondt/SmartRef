using SmartRef.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SmartRef.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Project> Projects { get; }
    DbSet<Customer> Customers { get; }
    DbSet<Contact> Contacts { get; }
    DbSet<Tag> Tags { get; }
    DbSet<Country> Countries { get; }
    DbSet<Channel> Channels { get; }
    DbSet<Agreement> Agreements { get; }
    DbSet<Sector> Sectors { get; }
    DbSet<Vertical> Verticals { get; }
    DbSet<TargetWork> TargetsWork { get; }
    DbSet<Technology> Technologies { get; }
    DbSet<TribeOffer> TribeOffers { get; }
    DbSet<ProjectTribeOffers> ProjectTribeOffers { get; }
    DbSet<ProjectContacts> ProjectContacts { get; }
    DbSet<ProjectTags> ProjectTags { get; }
    DbSet<ProjectCountries> ProjectCountries { get; }
    DbSet<ProjectTechnologies> ProjectTechnologies { get; }
    DbSet<ProjectChannels> ProjectChannels { get; }
    DbSet<ProjectTargetsWork> ProjectTargetsWork { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
