using Microsoft.AspNetCore.Mvc;
using SmartRef.Application.Common.DTOs;
using SmartRef.Application.Countries.Commands.CreateCountry;
using SmartRef.Application.Countries.Commands.CreateProjectCountry;
using SmartRef.Application.Countries.Commands.DeleteCountry;
using SmartRef.Application.Countries.Queries;

namespace SmartRef.WebUI.Controllers;

public class CountriesController : ApiControllerBase
{

    [HttpGet]
    public async Task<IEnumerable<CountryDTO>> GetCountries()
    {
        return await Mediator.Send(new GetCountriesQuery());
    }

    [HttpGet("{id}")]
    public async Task<CountryDTO> GetCountry(int id)
    {
        return await Mediator.Send(new GetCountryQuery(id));
    }

    [HttpGet("project/{id}")]
    public async Task<IEnumerable<CountryDTO>> GetCountriesByProject(int id)
    {
        return await Mediator.Send(new GetCountriesByProjectQuery(id));
    }

    [HttpPost("projectCountry")]
    public async Task<ActionResult<int>> CreateProjectCountryLink(CreateProjectCountryCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateCountryCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteCountryCommand(id));

        return NoContent();
    }

}
