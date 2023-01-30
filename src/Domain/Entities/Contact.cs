namespace SmartRef.Domain.Entities;

public class Contact : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public List<ProjectContacts> ProjectContacts { get; set; } = new List<ProjectContacts>();

    public Contact(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

}
