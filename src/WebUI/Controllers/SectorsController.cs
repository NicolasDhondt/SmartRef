using Microsoft.AspNetCore.Mvc;
using SmartRef.Application.Common.DTOs;
using SmartRef.Application.Sectors.Commands.CreateSector;
using SmartRef.Application.Sectors.Commands.DeleteSector;
using SmartRef.Application.Sectors.Queries;

namespace SmartRef.WebUI.Controllers;

public class SectorsController : ApiControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<SectorDTO>> GetSectors()
    {
        return await Mediator.Send(new GetSectorsQuery());
    }

    [HttpGet("vertical/{id}")]
    public async Task<IEnumerable<SectorDTO>> GetSectorsByVertical(int id)
    {
        return await Mediator.Send(new GetSectorsByVerticalQuery(id));
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateSectorCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteSectorCommand(id));

        return NoContent();
    }

}
