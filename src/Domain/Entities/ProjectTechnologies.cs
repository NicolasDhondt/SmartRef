namespace SmartRef.Domain.Entities;

public class ProjectTechnologies
{
    public int ProjectId { get; set; }
    public Project? Project { get; set; }
    public int TechnologyId { get; set; }
    public Technology? Technology { get; set; }

    public ProjectTechnologies(int projectId, int technologyId)
    {
        ProjectId = projectId;
        TechnologyId = technologyId;
    }
}
