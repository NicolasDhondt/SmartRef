namespace SmartRef.Domain.Entities;

public class ProjectCountries
{
    public int ProjectId { get; set; }
    public Project? Project { get; set; }
    public int CountryId { get; set; }
    public Country? Country { get; set; }

    public ProjectCountries(int projectId, int countryId)
    {
        ProjectId = projectId;
        CountryId = countryId;
    }
}
