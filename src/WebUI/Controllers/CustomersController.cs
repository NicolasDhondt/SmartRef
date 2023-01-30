using Application.Customers.Queries;
using Microsoft.AspNetCore.Mvc;
using SmartRef.Application.Common.DTOs;
using SmartRef.Application.Customers.Commands.CreateCustomer;
using SmartRef.Application.Customers.Commands.DeleteCustomer;
using SmartRef.Application.Customers.Commands.UpdateCustomer;
using SmartRef.Application.Customers.Queries;

namespace SmartRef.WebUI.Controllers;

public class CustomersController : ApiControllerBase
{

    [HttpGet]
    public async Task<IEnumerable<CustomerDTO>> GetCustomers()
    {
        return await Mediator.Send(new GetCustomersQuery());
    }

    [HttpGet("sector/{sectorId}")]
    public async Task<IEnumerable<CustomerDTO>> GetCustomersBy(int sectorId)
    {
        return await Mediator.Send(new GetCustomersBySectorQuery(sectorId));
    }

    [HttpGet("{id}")]
    public async Task<CustomerDTO> GetCustomer(int id)
    {
        return await Mediator.Send(new GetCustomerQuery(id));
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateCustomerCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateCustomerCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteCustomerCommand(id));

        return NoContent();
    }

}