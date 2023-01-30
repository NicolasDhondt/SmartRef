namespace SmartRef.Domain.Entities;

public class Channel : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public List<ProjectChannels> ProjectChannels { get; set; } = new List<ProjectChannels>();
}
