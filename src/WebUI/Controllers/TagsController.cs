using Microsoft.AspNetCore.Mvc;
using SmartRef.Application.Common.DTOs;
using SmartRef.Application.Tags.Commands.CreateProjectTag;
using SmartRef.Application.Tags.Commands.CreateTag;
using SmartRef.Application.Tags.Commands.DeleteTag;
using SmartRef.Application.Tags.Queries;

namespace SmartRef.WebUI.Controllers;

public class TagsController : ApiControllerBase
{

    [HttpGet]
    public async Task<IEnumerable<TagDTO>> GetTags()
    {
        return await Mediator.Send(new GetTagsQuery());
    }

    [HttpGet("project/{id}")]
    public async Task<IEnumerable<TagDTO>> GetTagsByProject(int id)
    {
        return await Mediator.Send(new GetTagsByProjectQuery(id));
    }

    [HttpPost("projectTag")]
    public async Task<ActionResult<int>> CreateProjectTagLink(CreateProjectTagCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateTagCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteTagCommand(id));

        return NoContent();
    }

}
