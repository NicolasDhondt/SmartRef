namespace SmartRef.Domain.Entities;

public class Sector : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public int VerticalId { get; set; }
    public Vertical? Vertical { get; set; }
    public List<Customer> Customers { get; set; } = new List<Customer>();

    public Sector(string name, int verticalId)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        VerticalId = verticalId;
    }

}
