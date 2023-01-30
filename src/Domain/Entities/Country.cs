namespace SmartRef.Domain.Entities;

public class Country : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Initial { get; set; } = string.Empty;
    public List<ProjectCountries> ProjectCountries { get; set; } = new List<ProjectCountries>();

    public Country(string name, string initial)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Initial = initial ?? throw new ArgumentNullException(nameof(initial));
    }

}
