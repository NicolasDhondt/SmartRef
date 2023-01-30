using System.Reflection;
using SmartRef.Application.Common.Interfaces;
using SmartRef.Domain.Entities;
using SmartRef.Infrastructure.Identity;
using SmartRef.Infrastructure.Persistence.Interceptors;
using Duende.IdentityServer.EntityFramework.Options;
using MediatR;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace SmartRef.Infrastructure.Persistence;

public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext
{
    private readonly IMediator _mediator;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IOptions<OperationalStoreOptions> operationalStoreOptions,
        IMediator mediator,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) 
        : base(options, operationalStoreOptions)
    {
        _mediator = mediator;
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Contact> Contacts => Set<Contact>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<Country> Countries => Set<Country>();
    public DbSet<Channel> Channels => Set<Channel>();
    public DbSet<Agreement> Agreements => Set<Agreement>();
    public DbSet<Sector> Sectors => Set<Sector>();
    public DbSet<Vertical> Verticals => Set<Vertical>();
    public DbSet<TargetWork> TargetsWork => Set<TargetWork>();
    public DbSet<Technology> Technologies => Set<Technology>();
    public DbSet<TribeOffer> TribeOffers => Set<TribeOffer>();

    public DbSet<ProjectTribeOffers> ProjectTribeOffers => Set<ProjectTribeOffers>();
    public DbSet<ProjectContacts> ProjectContacts => Set<ProjectContacts>();
    public DbSet<ProjectTags> ProjectTags => Set<ProjectTags>();
    public DbSet<ProjectCountries> ProjectCountries => Set<ProjectCountries>();
    public DbSet<ProjectTechnologies> ProjectTechnologies => Set<ProjectTechnologies>();
    public DbSet<ProjectChannels> ProjectChannels => Set<ProjectChannels>();
    public DbSet<ProjectTargetsWork> ProjectTargetsWork => Set<ProjectTargetsWork>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);

        return await base.SaveChangesAsync(cancellationToken);
    }
}
