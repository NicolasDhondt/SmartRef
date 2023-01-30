namespace SmartRef.Domain.Entities;

public class Tag : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public List<ProjectTags> ProjectTags { get; set; } = new List<ProjectTags>();

    public Tag(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
    
}
