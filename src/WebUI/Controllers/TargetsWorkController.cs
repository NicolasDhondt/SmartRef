using Microsoft.AspNetCore.Mvc;
using SmartRef.Application.Common.DTOs;
using SmartRef.Application.TargetsWork.Commands.CreateProjectTargetsWork;
using SmartRef.Application.TargetsWork.Commands.CreateTargetWork;
using SmartRef.Application.TargetsWork.Commands.DeleteTargetWork;
using SmartRef.Application.TargetsWork.Queries;

namespace SmartRef.WebUI.Controllers;

public class TargetsWorkController : ApiControllerBase
{

    [HttpGet]
    public async Task<IEnumerable<TargetWorkDTO>> GetTargetsWorkDTO()
    {
        return await Mediator.Send(new GetTargetsWorkQuery());
    }

    [HttpGet("project/{id}")]
    public async Task<IEnumerable<TargetWorkDTO>> GetTargetsWorkByProject(int id)
    {
        return await Mediator.Send(new GetTargetsWorkByProjectQuery(id));
    }

    [HttpPost("projectTargetWork")]
    public async Task<ActionResult<int>> CreateProjectTargetWorkDTOLink(CreateProjectTargetWorkCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateTargetWorkCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteTargetWorkCommand(id));

        return NoContent();
    }

}
