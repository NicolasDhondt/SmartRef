using Microsoft.AspNetCore.Mvc;
using SmartRef.Application.Common.DTOs;
using SmartRef.Application.TribeOffers.Commands.CreateTribeOffer;
using SmartRef.Application.TribeOffers.Commands.DeleteTribeOffer;
using SmartRef.Application.TribeOffers.Queries;

namespace SmartRef.WebUI.Controllers;

public class TribeOffersController : ApiControllerBase
{

    [HttpGet]
    public async Task<IEnumerable<TribeOfferDTO>> GetTribeOffers()
    {
        return await Mediator.Send(new GetTribeOffersQuery());
    }

    [HttpGet("project/{id}")]
    public async Task<IEnumerable<TribeOfferDTO>> GetTribeOffersByProject(int id)
    {
        return await Mediator.Send(new GetTribeOffersByProjectQuery(id));
    }

    [HttpPost("projectTribeOffer")]
    public async Task<ActionResult<int>> CreateProjectTribeOfferLink(CreateProjectTribeOfferCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateTribeOfferCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteTribeOfferCommand(id));

        return NoContent();
    }

}
