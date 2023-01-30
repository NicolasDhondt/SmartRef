namespace SmartRef.Domain.Entities;

public class Technology : BaseEntity
{

    public string Name { get; set; } = string.Empty;
    public List<ProjectTechnologies> ProjectTechnologies { get; set; } = new List<ProjectTechnologies>();

    public Technology(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

}
