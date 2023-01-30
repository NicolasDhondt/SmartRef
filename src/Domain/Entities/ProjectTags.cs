namespace SmartRef.Domain.Entities;

public class ProjectTags
{
    public int ProjectId { get; set; }
    public Project? Project { get; set; }
    public int TagId { get; set; }
    public Tag? Tag { get; set; }
}
