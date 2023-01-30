namespace SmartRef.Domain.Entities;

public class Agreement : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public int ProjectId { get; set; }
    public Project? Project { get; set; }
}
