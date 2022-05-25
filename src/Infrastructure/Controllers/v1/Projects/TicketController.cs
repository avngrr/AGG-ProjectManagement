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
public class TicketController : BaseController
{
    public TicketController(IMediator mediator) : base(mediator)
    {
        
    }

    [Authorize(Policy = Permissions.Tickets.View)]
    [HttpGet("project/{projectId}")]
    public async Task<IActionResult> GetByProject(int projectId)
    {
        var result = await _mediator.Send(new GetTicketsForProjectQuery() {ProjectId = projectId});
        return Ok(result);
    }

    [Authorize(Policy = Permissions.Tickets.View)]
    [HttpGet("{ticketId}")]
    public async Task<IActionResult> Get(int ticketId)
    {
        var result = await _mediator.Send(new GetByIdTicketCommand() { Id = ticketId });
        return Ok(result);
    }
    
    [Authorize(Policy = Permissions.Tickets.Create)]
    [HttpPost]
    public async Task<IActionResult> Post(AddEditTicketCommand command)
    {
        var result = await _mediator.Send(command);
        return result.IsSuccess ? Ok() : NotFound(result.Reasons[0]);
    }
}