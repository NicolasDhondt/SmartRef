using Microsoft.AspNetCore.Mvc;
using SmartRef.Application.Agreements.Commands.CreateAgreement;
using SmartRef.Application.Agreements.Commands.DeleteAgreement;
using SmartRef.Application.Agreements.Queries;
using SmartRef.Application.Common.DTOs;

namespace SmartRef.WebUI.Controllers;

public class AgreementsController : ApiControllerBase
{

    [HttpGet("project/{id}")]
    public async Task<IEnumerable<AgreementDTO>> GetAgreementsByProject(int id)
    {
        return await Mediator.Send(new GetAgreementsByProjectQuery(id));
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateAgreementCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteAgreementCommand(id));

        return NoContent();
    }

}
