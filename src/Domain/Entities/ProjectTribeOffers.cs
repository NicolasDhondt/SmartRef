namespace SmartRef.Domain.Entities;

public class ProjectTribeOffers
{
    public int ProjectId { get; set; }
    public Project? Project { get; set; }
    public int TribeOfferId { get; set; }
    public TribeOffer? TribeOffer { get; set; }

    public ProjectTribeOffers(int projectId, int tribeOfferId)
    {
        ProjectId = projectId;
        TribeOfferId = tribeOfferId;
    }
}
