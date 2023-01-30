using Microsoft.AspNetCore.Mvc;
using SmartRef.Application.Common.DTOs;
using SmartRef.Application.Common.Models;
using SmartRef.Application.Projects.Commands.CreateProject;
using SmartRef.Application.Projects.Commands.DeleteProject;
using SmartRef.Application.Projects.Commands.UpdateProject;
using SmartRef.Application.Projects.Queries;
using SmartRef.Infrastructure.Files;

namespace SmartRef.WebUI.Controllers;

public class ProjectsController : ApiControllerBase
{

    private readonly IConfiguration config;
    private const string EXCEL_NAME = "ReferenceOverview.xlsx";
    private const string PPT_BASE = "Ref_Base_Template.pptx";

    public ProjectsController(IConfiguration config)
    {
        this.config = config;
    }

    [HttpGet("page/{pageNumber}")]
    public async Task<PaginatedList<ProjectDTO>> GetProjects(int pageNumber, int pageSize,
    [FromQuery] string? searchText, [FromQuery] string? verticalId, [FromQuery] string? sectorId, 
    [FromQuery] string? customerId, [FromQuery] string? technologyId, [FromQuery] string? countryId, 
    [FromQuery] string? offerId, [FromQuery] string? targetId, [FromQuery] Boolean isPrivate, [FromQuery] Boolean isPublic)
    {
        return await Mediator.Send(new GetProjectsQuery()
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            SearchText = searchText,
            VerticalId = verticalId,
            SectorId = sectorId,
            CustomerId = customerId,
            CountryId = countryId,
            OfferId = offerId,
            TargetId = targetId,
            TechnologyId = technologyId,
            isPrivate = isPrivate,
            isPublic = isPublic
        });
    }

    [HttpGet("{id}")]
    public async Task<ProjectDTO> GetProject(int id)
    {
        return await Mediator.Send(new GetProjectQuery(id));
    }

    [HttpGet("download/{projectId}")]
    public async Task<ActionResult> DownloadPPTXFile(int projectId)
    {
        var project = await Mediator.Send(new GetProjectQuery(projectId));
        string fileName = "Ref_" + project.Customer?.Name.Replace(" ", "_") + ".pptx";
        PowerPointGenerator powerPointGenerator = new();
        powerPointGenerator.CreatePackage(fileName, project);
        var bytes = await System.IO.File.ReadAllBytesAsync(fileName);
        FileInfo file = new FileInfo(fileName);  
        file.Delete();  
        return File(bytes, "application/x-mspowerpoint", fileName);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateProjectCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, UpdateProjectCommand command)
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
        await Mediator.Send(new DeleteProjectCommand(id));

        return NoContent();
    }

}