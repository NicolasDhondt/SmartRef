using Microsoft.AspNetCore.Mvc;
using SmartRef.Application.Common.DTOs;
using SmartRef.Application.Technologies.Commands.CreateProjectTechnology;
using SmartRef.Application.Technologies.Commands.CreateTechnology;
using SmartRef.Application.Technologies.Commands.DeleteTechnology;
using SmartRef.Application.Technologies.Queries;

namespace SmartRef.WebUI.Controllers;

public class TechnologiesController : ApiControllerBase
{

    [HttpGet]
    public async Task<IEnumerable<TechnologyDTO>> GetTechnologies()
    {
        return await Mediator.Send(new GetTechnologiesQuery());
    }

    [HttpGet("{id}")]
    public async Task<TechnologyDTO> GetTechnology(int id)
    {
        return await Mediator.Send(new GetTechnologyQuery(id));
    }

    [HttpGet("project/{id}")]
    public async Task<IEnumerable<TechnologyDTO>> GetTechnologiesByProject(int id)
    {
        return await Mediator.Send(new GetTechnologiesByProjectQuery(id));
    }

    [HttpPost("projectTechnology")]
    public async Task<ActionResult<int>> CreateProjectTechnologyLink(CreateProjectTechnologyCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateTechnologyCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteTechnologyCommand(id));

        return NoContent();
    }

}
