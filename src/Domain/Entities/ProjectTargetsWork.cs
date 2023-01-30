namespace SmartRef.Domain.Entities;

public class ProjectTargetsWork
{
    public int ProjectId { get; set; }
    public Project? Project { get; set; }
    public int TargetWorkId { get; set; }
    public TargetWork? TargetWork { get; set; }
    
    public ProjectTargetsWork(int projectId, int targetWorkId)
    {
        ProjectId = projectId;
        TargetWorkId = targetWorkId;
    }
}
