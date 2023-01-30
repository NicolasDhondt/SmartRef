using Microsoft.AspNetCore.Mvc;
using SmartRef.Application.Common.DTOs;
using SmartRef.Application.Contacts.Commands.CreateContact;
using SmartRef.Application.Contacts.Commands.CreateProjectContact;
using SmartRef.Application.Contacts.Commands.DeleteContact;
using SmartRef.Application.Contacts.Queries;
using SmartRef.Domain.Enums;

namespace SmartRef.WebUI.Controllers;

public class ContactsController : ApiControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<ContactDTO>> GetContacts()
    {
        return await Mediator.Send(new GetContactsQuery());
    }

    [HttpGet("{id}")]
    public async Task<ContactDTO> GetContact(int id)
    {
        return await Mediator.Send(new GetContactQuery(id));
    }

    [HttpGet("project/{id}")]
    public async Task<IEnumerable<ContactDTO>> GetContactsByProject(int id)
    {
        return await Mediator.Send(new GetContactsByProjectQuery(id));
    }

    [HttpGet("type/{type}")]
    public async Task<IEnumerable<ContactDTO>> GetContactsByType(ContactType type)
    {
        return await Mediator.Send(new GetContactsByTypeQuery(type));
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateContactCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost("projectContact")]
    public async Task<ActionResult<int>> CreateProjectContactLink(CreateProjectContactCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteContactCommand(id));

        return NoContent();
    }

}