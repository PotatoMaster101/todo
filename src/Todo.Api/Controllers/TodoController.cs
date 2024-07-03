using FluentResults.Extensions.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Requests;
using Todo.Features.Todo.CreateTodo;
using Todo.Features.Todo.DeleteTodo;
using Todo.Features.Todo.GetTodo;
using Todo.Features.Todo.UpdateTodo;

namespace Todo.Api.Controllers;

/// <summary>
/// Represents the controller for TODO items.
/// </summary>
[ApiController]
[Authorize]
[Tags("TODO")]
public class TodoController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly UserManager<IdentityUser> _userManager;

    /// <summary>
    /// Constructs a new instance of <see cref="TodoController"/>
    /// </summary>
    public TodoController(UserManager<IdentityUser> userManager, IMediator mediator)
    {
        _userManager = userManager;
        _mediator = mediator;
    }

    /// <summary>
    /// Route for creating a new TODO item.
    /// </summary>
    /// <param name="request">The item request.</param>
    /// <returns>The created TODO item.</returns>
    [HttpPut("/todos")]
    public async Task<IActionResult> Create([FromBody] CreateTodoRequest request)
    {
        var userId = _userManager.GetUserId(User);
        if (userId is null)
            return BadRequest();

        var query = new CreateTodoCommand(request.Message, request.Title, userId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Route for deleting a TODO item.
    /// </summary>
    /// <param name="request">The item request.</param>
    /// <returns>The deleted TODO item.</returns>
    [HttpDelete("/todos")]
    public async Task<IActionResult> Delete([FromBody] DeleteTodoRequest request)
    {
        var userId = _userManager.GetUserId(User);
        if (userId is null)
            return Problem("User not found", statusCode: StatusCodes.Status400BadRequest);

        var query = new DeleteTodoCommand(request.Id, userId);
        var result = await _mediator.Send(query);
        return result.ToActionResult();
    }

    /// <summary>
    /// Route for getting TODO items under the current user.
    /// </summary>
    /// <returns>The TODO items under the current user.</returns>
    [HttpGet("/todos")]
    public async Task<IActionResult> Get()
    {
        var userId = _userManager.GetUserId(User);
        if (userId is null)
            return BadRequest();

        var query = new GetTodoByUserQuery(userId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Route for getting a TODO item using a specific ID.
    /// </summary>
    /// <returns>The TODO items with the specific ID.</returns>
    [HttpGet("/todos/{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var userId = _userManager.GetUserId(User);
        if (userId is null)
            return BadRequest();

        var query = new GetTodoByIdQuery(id, userId);
        var result = await _mediator.Send(query);
        return result.ToActionResult();
    }

    /// <summary>
    /// Route for updating a TODO item.
    /// </summary>
    /// <param name="request">The item request.</param>
    /// <returns>The TODO items under the current user.</returns>
    [HttpPost("/todos")]
    public async Task<IActionResult> Update([FromBody] UpdateTodoRequest request)
    {
        var userId = _userManager.GetUserId(User);
        if (userId is null)
            return BadRequest();

        var query = new UpdateTodoCommand(request.Done, request.Id, request.Message, request.Title, userId);
        var result = await _mediator.Send(query);
        return result.ToActionResult();
    }
}
