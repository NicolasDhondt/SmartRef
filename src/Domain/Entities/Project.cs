namespace SmartRef.Domain.Entities;

public class Project : BaseAuditableEntity
{
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
    public float DataQualityPercent { get { return ComputeDataQuality(); } }
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

    public Project(string name, int endYear, bool isFinished, decimal price, int manDay, string? solutions,
        string? issues, string? benefits, string? narative, bool isPublic, string? photoUrl, int customerId)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        EndYear = endYear;
        IsFinished = isFinished;
        Price = price;
        ManDay = manDay;
        Solutions = solutions;
        Issues = issues;
        Benefits = benefits;
        Narative = narative;
        IsPublic = isPublic;
        PhotoUrl = photoUrl;
        CustomerId = customerId;
    }

    public Project(string name, int endYear, bool isFinished, decimal price, int manDay, string? narative, 
        bool isPublic, string? photoUrl, int customerId)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        EndYear = endYear;
        IsFinished = isFinished;
        Price = price;
        ManDay = manDay;
        Narative = narative;
        IsPublic = isPublic;
        PhotoUrl = photoUrl;
        CustomerId = customerId;
    }

    private float ComputeDataQuality()
    {
        int totalSize = 0;
        if (!string.IsNullOrWhiteSpace(Solutions))
            totalSize += Solutions.Length;
        if (!string.IsNullOrWhiteSpace(Issues))
            totalSize += Issues.Length;
        if (!string.IsNullOrWhiteSpace(Benefits))
            totalSize += Benefits.Length;
        return (totalSize * 100) / 1140; // 1140 is the max size possible, 380 per params
    }

}