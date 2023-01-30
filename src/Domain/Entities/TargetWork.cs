namespace SmartRef.Domain.Entities;

public class TargetWork : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public List<ProjectTargetsWork> ProjectTargetsWork { get; set; } = new List<ProjectTargetsWork>();

    public TargetWork(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
}
