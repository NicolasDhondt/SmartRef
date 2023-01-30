using Microsoft.AspNetCore.Mvc;
using SmartRef.Application.Common.DTOs;
using SmartRef.Application.Verticals.Commands.CreateVertical;
using SmartRef.Application.Verticals.Commands.DeleteVertical;
using SmartRef.Application.Verticals.Queries;

namespace SmartRef.WebUI.Controllers;

public class VerticalsController : ApiControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<VerticalDTO>> GetVerticals()
    {
        return await Mediator.Send(new GetVerticalsQuery());
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateVerticalCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteVerticalCommand(id));

        return NoContent();
    }

}
