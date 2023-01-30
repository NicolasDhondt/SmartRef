namespace SmartRef.Domain.Entities;

public class TribeOffer : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public List<ProjectTribeOffers> ProjectTribeOffers { get; set; } = new List<ProjectTribeOffers>();

    public TribeOffer(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
}
