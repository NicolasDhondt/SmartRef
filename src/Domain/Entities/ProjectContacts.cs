namespace SmartRef.Domain.Entities;

public class ProjectContacts
{
    public int ProjectId { get; set; }
    public Project? Project { get; set; }
    public int ContactId { get; set; }
    public Contact? Contact { get; set; }
    public ContactType ContactType { get; set; }

    public ProjectContacts(int projectId, int contactId, ContactType contactType)
    {
        ProjectId = projectId;
        ContactId = contactId;
        ContactType = contactType;
    }
}
