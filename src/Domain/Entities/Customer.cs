namespace SmartRef.Domain.Entities;

public class Customer : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? PhotoUrl { get; set; } = null;
    public int VerticalId { get; set; }
    public Vertical? Vertical { get; set; }
    public int SectorId { get; set; }
    public Sector? Sector { get; set; }
    public List<Project> Projects { get; set; } = new List<Project>();

    public Customer(string name, string? photoUrl, int verticalId, int sectorId) // used in the create command
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        PhotoUrl = photoUrl;
        VerticalId = verticalId;
        SectorId = sectorId;
    }

    public Customer(string name, int verticalId, int sectorId) // used in the excel parser
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        VerticalId = verticalId;
        SectorId = sectorId;
    }
}
