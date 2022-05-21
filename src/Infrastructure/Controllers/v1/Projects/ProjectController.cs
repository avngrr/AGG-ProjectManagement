﻿using Application.Common.Permissions;
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
    [Authorize(Policy = Permissions.Projects.View)]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllProjectsQuery());
        return Ok(result);
    }
    [Authorize(Policy = Permissions.Projects.View)]
    [HttpGet("{projectId}")]
    public async Task<IActionResult> GetById(int projectId)
    {
        var result = await _mediator.Send(new GetByIdProjectsQuery() { Id = projectId });
        return Ok(result);
    }
    [Authorize(Policy = Permissions.Projects.Create)]
    [HttpPost]
    public async Task<IActionResult> Post(AddEditProjectCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
    [Authorize(Policy = Permissions.Projects.Edit)]
    [HttpPost("complete")]
    public async Task<IActionResult> Post(CompleteProjectCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
    [Authorize(Policy = Permissions.Projects.Edit)]
    [HttpPost("reset")]
    public async Task<IActionResult> Post(ResetCompleteCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
    [Authorize(Policy = Permissions.Projects.Delete)]
    [HttpDelete]
    public async Task<IActionResult> Post(DeleteProjectCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
}