namespace SmartRef.Domain.Entities;

public class ProjectChannels
{
    public int ProjectId { get; set; }
    public Project? Project { get; set; }
    public int ChannelId { get; set; }
    public Channel? Channel { get; set; }
}
