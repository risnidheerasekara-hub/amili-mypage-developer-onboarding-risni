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
    public async Task<ActionResult> CreateTodoItem([FromBody] CreateTodo request)
    {
        var response = await todoService.CreateTodoAsync(request);
        return CreatedAtAction(nameof(GetTodoItemById), new { id = response.Id }, response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TodoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetTodoItemById(long id)
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
    public async Task<ActionResult> GetAllTodoItems()
    {
        var response = await todoService.GetAllTodosAsync();

        return Ok(response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(TodoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateTodoItem(long id, [FromBody] UpdateTodo request)
    {
        var response = await todoService.UpdateTodoAsync(id, request);
        if (response == null)
        {
            return NotFound();
        }
        return Ok(response);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<string?> DeleteTodoItem(long id)
    {

        var deleteResponse = await todoService.DeleteTodoAsync(id);
        return deleteResponse;
    }

}