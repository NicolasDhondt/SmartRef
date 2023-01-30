namespace SmartRef.Domain.Entities;

public class Vertical : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public List<Sector> Sectors { get; set; } = new List<Sector>();
    public List<Customer> Customers { get; set; } = new List<Customer>();

    public Vertical(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
}
