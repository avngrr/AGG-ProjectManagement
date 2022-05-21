using Application.Common.Permissions;
using Application.Projects.Commands;
using Application.Projects.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Controllers.v1.Projects;

[ApiController]
[Route("api/[controller]")]
[ApiVersion("1.0")]
public class ProjectController : BaseController
{
    public ProjectController(IMediator mediator) : base(mediator)
    {
        
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllProjectsCommand());
        return Ok(result);
    }
    
    [Authorize(Policy = Permissions.Projects.Create)]
    [HttpPost]
    public async Task<IActionResult> Post(AddEditProjectCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
}