using Microsoft.AspNetCore.Mvc;
using SmartRef.Application.Channels.Commands.CreateChannel;
using SmartRef.Application.Channels.Commands.CreateProjectChannel;
using SmartRef.Application.Channels.Commands.DeleteChannel;
using SmartRef.Application.Channels.Queries;
using SmartRef.Application.Common.DTOs;

namespace SmartRef.WebUI.Controllers;

public class ChannelsController : ApiControllerBase
{

    [HttpGet]
    public async Task<IEnumerable<ChannelDTO>> GetChannels()
    {
        return await Mediator.Send(new GetChannelsQuery());
    }

    [HttpGet("project/{id}")]
    public async Task<IEnumerable<ChannelDTO>> GetChannelsByProject(int id)
    {
        return await Mediator.Send(new GetChannelsByProjectQuery(id));
    }

    [HttpPost("projectChannel")]
    public async Task<ActionResult<int>> CreateProjectChannelLink(CreateProjectChannelCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateChannelCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteChannelCommand(id));

        return NoContent();
    }

}
