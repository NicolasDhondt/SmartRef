using SmartRef.Domain.Entities;
using SmartRef.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using SmartRef.Infrastructure.Files;
using Microsoft.Extensions.Configuration;

namespace SmartRef.Infrastructure.Persistence;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _config;
    private ExcelReferencesReader _excelReferencesReader;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, 
        UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _config = config;
        _excelReferencesReader = new(_config);
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            // await TrySeedAsync();
            await ReadReferencesExcel();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var administratorRole = new IdentityRole("Administrator");

        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await _roleManager.CreateAsync(administratorRole);
        }

        // Default users
        var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "Administrator1!");
            await _userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
        }

        if (!await _context.Agreements.AnyAsync())
        {
            var agreementsData = await File.ReadAllTextAsync("../Infrastructure/Files/Seeds/SeedAgreement.json");
            var agreements = JsonSerializer.Deserialize<List<Agreement>>(agreementsData);
            if (agreements != null)
            {
                foreach (var agreement in agreements)
                {
                    _context.Agreements.Add(agreement);
                }
                await _context.SaveChangesAsync();
            }
        }

        if (!await _context.Channels.AnyAsync())
        {
            var channelsData = await File.ReadAllTextAsync("../Infrastructure/Files/Seeds/SeedChannel.json");
            var channels = JsonSerializer.Deserialize<List<Channel>>(channelsData);
            if (channels != null)
            {
                foreach (var channel in channels)
                {
                    _context.Channels.Add(channel);
                }
                await _context.SaveChangesAsync();
            }
        }

        if (!await _context.ProjectChannels.AnyAsync())
        {
            var projectChannelsData = await File.ReadAllTextAsync("../Infrastructure/Files/Seeds/SeedProjectChannels.json");
            var projectChannels = JsonSerializer.Deserialize<List<ProjectChannels>>(projectChannelsData);
            if (projectChannels != null)
            {
                foreach (var projectChannel in projectChannels)
                {
                    _context.ProjectChannels.Add(projectChannel);
                }
                await _context.SaveChangesAsync();
            }
        }

    }

    private async Task ReadReferencesExcel()
    {
        if (!await _context.Verticals.AnyAsync())
        {
            foreach (var vertical in _excelReferencesReader.GetVerticals())
                _context.Verticals.Add(vertical);
            await _context.SaveChangesAsync();
        }

        if (!await _context.Sectors.AnyAsync())
        {
            foreach (var vertical in await _context.Verticals.ToListAsync())
                foreach (var sector in _excelReferencesReader.GetSectorsByVertical(vertical))
                    _context.Sectors.Add(sector);
            await _context.SaveChangesAsync();
        }

        if (!await _context.Customers.AnyAsync())
        {
            foreach (var sector in await _context.Sectors.ToListAsync())
                foreach (var customer in _excelReferencesReader.GetCustomersBySector(sector))
                    _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }

        if (!await _context.Contacts.AnyAsync())
        {
            foreach (var contact in _excelReferencesReader.GetContacts())
                _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
        }

        if (!await _context.Technologies.AnyAsync()) {
            foreach (var technology in _excelReferencesReader.GetTechnologies())
                _context.Technologies.Add(technology);
            await _context.SaveChangesAsync();
        }

        // Only 2 Targets Work
        _context.TargetsWork.Add(new TargetWork("Digital"));
        _context.TargetsWork.Add(new TargetWork("Data"));
        await _context.SaveChangesAsync(); // convert into simple seeder?

        if (!await _context.Countries.AnyAsync())
        {
            foreach (var country in _excelReferencesReader.GetCountries())
                _context.Countries.Add(country);
            await _context.SaveChangesAsync();
        }

        // Only 4 Tribe Offers
        _context.TribeOffers.Add(new TribeOffer("New"));
        _context.TribeOffers.Add(new TribeOffer("More"));
        _context.TribeOffers.Add(new TribeOffer("Better"));
        _context.TribeOffers.Add(new TribeOffer("Right"));
        await _context.SaveChangesAsync(); // convert into simple seeder?

        // Need more infos about it
        _context.Tags.Add(new Tag("CONSULTING"));
        _context.Tags.Add(new Tag("IMPLEMENTATION"));
        _context.Tags.Add(new Tag("LICENSE RESSELL"));
        _context.Tags.Add(new Tag("MANAGED SERVICES"));
        _context.Tags.Add(new Tag("OTHER"));
        await _context.SaveChangesAsync(); // convert into simple seeder?

        if (!await _context.Projects.AnyAsync())
        {
            foreach (var customer in await _context.Customers.ToListAsync())
                foreach (var project in _excelReferencesReader.GetProjectsByCustomer(customer))
                    _context.Projects.Add(project);
            await _context.SaveChangesAsync();
        }

        if (!await _context.ProjectTribeOffers.AnyAsync())
        {
            foreach (var tribeOffer in await _context.TribeOffers.ToListAsync())
                foreach (var project in await _context.Projects.ToListAsync())
                    if (_excelReferencesReader.IsProjectLinkWith(project.Name, tribeOffer.Name, "G"))
                        _context.ProjectTribeOffers.Add(new ProjectTribeOffers(project.Id, tribeOffer.Id));
            await _context.SaveChangesAsync();
        }

        if (!await _context.ProjectTechnologies.AnyAsync())
        {
            foreach (var technology in await _context.Technologies.ToListAsync())
                foreach (var project in await _context.Projects.ToListAsync())
                    if (_excelReferencesReader.IsProjectLinkWith(project.Name, technology.Name, "M"))
                        _context.ProjectTechnologies.Add(new ProjectTechnologies(project.Id, technology.Id));
            await _context.SaveChangesAsync();
        }

        if (!await _context.ProjectCountries.AnyAsync())
        {
            foreach (var country in await _context.Countries.ToListAsync())
                foreach (var project in await _context.Projects.ToListAsync())
                    if (_excelReferencesReader.IsProjectLinkWith(project.Name, country.Initial, "N"))
                        _context.ProjectCountries.Add(new ProjectCountries(project.Id, country.Id));
            await _context.SaveChangesAsync();
        }

        if (!await _context.ProjectTargetsWork.AnyAsync())
        {
            foreach (var targetWork in await _context.TargetsWork.ToListAsync())
                foreach (var project in await _context.Projects.ToListAsync())
                    if (_excelReferencesReader.IsProjectLinkWith(project.Name, targetWork.Name, "H"))
                        _context.ProjectTargetsWork.Add(new ProjectTargetsWork(project.Id, targetWork.Id));
            await _context.SaveChangesAsync();
        }

        if (!await _context.ProjectContacts.AnyAsync())
        {
            foreach (var contact in await _context.Contacts.ToListAsync())
                foreach (var project in await _context.Projects.ToListAsync())
                {
                    if (_excelReferencesReader.IsProjectLinkWith(project.Name, contact.Name, "O"))
                        _context.ProjectContacts.Add(new ProjectContacts(project.Id, contact.Id, Domain.Enums.ContactType.DELIVERY));
                    if (_excelReferencesReader.IsProjectLinkWith(project.Name, contact.Name, "P"))
                        _context.ProjectContacts.Add(new ProjectContacts(project.Id, contact.Id, Domain.Enums.ContactType.SALES));
                }
            await _context.SaveChangesAsync();
        }

    }

}
