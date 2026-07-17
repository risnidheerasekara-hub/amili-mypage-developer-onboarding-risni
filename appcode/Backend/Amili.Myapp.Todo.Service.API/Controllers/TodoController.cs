using Amili.Myapp.Todo.Service.Core.Models.Request;
using Amili.Myapp.Todo.Service.Core.Models.Response;
using Amili.Myapp.Todo.Service.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Amili.Myapp.Todo.Service.API.Controller;


[Route("api/[controller]")]
[ApiController]
public class TodoItemsController(ITodoService todoService) : ControllerBase
{

    [HttpPost]
    [ProducesResponseType(typeof(TodoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTodoItem([FromBody] CreateTodoRequest request)
    {
        var response = await todoService.CreateTodoAsync(request);
        return Created($"/api/todos/{response.Id}", response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TodoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTodoItemById(long id)
    {
        var response = await todoService.GetTodoByIdAsync(id);
        if (response == null)
        {
            return NotFound();
        }
        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TodoResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllTodoItems()
    {
        var response = await todoService.GetAllTodosAsync();

        return Ok(response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(TodoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateTodoItem(long id, [FromBody] UpdateTodoRequest request)
    {
        var response = await todoService.UpdateTodoAsync(id, request);
        if (response == null)
        {
            return NotFound();
        }
        return Ok(response);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTodoItem(long id)
    {

        var deleted = await todoService.DeleteTodoAsync(id);
        if (!deleted)
        {
            return NotFound();
        }
        return Ok(deleted);
    }

    [HttpPatch("{id}/complete")]
    [ProducesResponseType(typeof(TodoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CompleteTodoAsync(long id)
    {
        var response = await todoService.CompleteTodoAsync(id);
        if (response == null)
        {
            return NotFound();
        }
        return Ok(response);
    }

}