using Amili.Myapp.Todo.Service.API.Controller;
using Amili.Myapp.Todo.Service.Core.Models.Request;
using Amili.Myapp.Todo.Service.Core.Models.Response;
using Amili.Myapp.Todo.Service.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Amili.Myapp.Todo.Service.Test.API;

public class TodoControllerUnitTests
{
    private readonly Mock<ITodoService> _todoServiceMock;
    private readonly TodoItemsController _controller;

    public TodoControllerUnitTests()
    {
        _todoServiceMock = new Mock<ITodoService>();
        _controller = new TodoItemsController(_todoServiceMock.Object);
    }

    [Fact(DisplayName = "Create | Success")]
    public async Task Should_ReturnCreatedAtActionResult_When_CreateTodoItem_WithValidRequest()
    {
        var request = new CreateTodoRequest { Name = "Buy groceries" };
        var response = new TodoResponse { Id = 1, Name = request.Name };
        _todoServiceMock.Setup(s => s.CreateTodoAsync(request)).ReturnsAsync(response);

        var result = await _controller.CreateTodoItem(request);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(TodoItemsController.GetTodoItemById), createdResult.ActionName);
        Assert.Equal(response, createdResult.Value);
        _todoServiceMock.Verify(
        x => x.CreateTodoAsync(request),
        Times.Once);
    }

    [Fact(DisplayName = "Get todo by id | success ")]
    public async Task Should_ReturnOkObjectResult_When_GetTodoItemById_WithExistingId()
    {
        var id = 1;
        var response = new TodoResponse { Id = id, Name = "Buy groceries" };
        _todoServiceMock.Setup(s => s.GetTodoByIdAsync(id)).ReturnsAsync(response);
        var result = await _controller.GetTodoItemById(id);
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(response, okResult.Value);
        _todoServiceMock.Verify(
            x => x.GetTodoByIdAsync(id),
            Times.Once);
    }

    [Fact(DisplayName = "Get todo by id | not found")]
    public async Task Should_ReturnNotFoundResult_When_GetTodoItemById_WithNonExistingId()
    {
        var id = 999;
        _todoServiceMock.Setup(s => s.GetTodoByIdAsync(id)).ReturnsAsync((TodoResponse?)null);
        var result = await _controller.GetTodoItemById(id);
        Assert.IsType<NotFoundResult>(result);
        _todoServiceMock.Verify(
            x => x.GetTodoByIdAsync(id),
            Times.Once);
    }

    [Fact(DisplayName = "Get all todos | success")]
    public async Task Should_ReturnOkObjectResultWithAllTodos_When_GetAllTodoItemsIsInvoked()
    {
        var todos = new List<TodoResponse>
        {
            new TodoResponse { Id = 1, Name = "Buy groceries" },
            new TodoResponse { Id = 2, Name = "Walk the dog" }
        };
        _todoServiceMock.Setup(s => s.GetAllTodosAsync()).ReturnsAsync(todos.ToArray());
        var result = await _controller.GetAllTodoItems();
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedTodos = Assert.IsAssignableFrom<IEnumerable<TodoResponse>>(okResult.Value);
        Assert.Equal(todos.Count, returnedTodos.Count());
        _todoServiceMock.Verify(
            x => x.GetAllTodosAsync(),
            Times.Once);
    }

    [Fact(DisplayName = "Get all todos | no todos")]
    public async Task Should_ReturnOkObjectResultWithEmptyList_When_NoTodosExist()
    {
        _todoServiceMock.Setup(s => s.GetAllTodosAsync()).ReturnsAsync(Array.Empty<TodoResponse>());

        var result = await _controller.GetAllTodoItems();
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedTodos = Assert.IsAssignableFrom<IEnumerable<TodoResponse>>(okResult.Value);
        Assert.Empty(returnedTodos);
        _todoServiceMock.Verify(x => x.GetAllTodosAsync(), Times.Once);

    }

    [Fact(DisplayName = "Update todo | success")]
    public async Task Should_ReturnOkObjectResult_When_UpdateTodoItemWithExistingId()
    {
        var id = 1;
        var request = new UpdateTodoRequest { Name = "Buy groceries and cook dinner" };
        var response = new TodoResponse { Id = id, Name = request.Name };
        _todoServiceMock.Setup(s => s.UpdateTodoAsync(id, request)).ReturnsAsync(response);
        var result = await _controller.UpdateTodoItem(id, request);
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(response, okResult.Value);
        _todoServiceMock.Verify(x => x.UpdateTodoAsync(id, request), Times.Once);
    }

    [Fact(DisplayName = "Update todo | not found")]
    public async Task Should_ReturnNotFoundResult_When_UpdateTodoItemWithNonExistingId()
    {
        var id = 999;
        var request = new UpdateTodoRequest { Name = "Non-existing todo" };
        _todoServiceMock.Setup(s => s.UpdateTodoAsync(id, request)).ReturnsAsync((TodoResponse?)null);
        var result = await _controller.UpdateTodoItem(id, request);
        Assert.IsType<NotFoundResult>(result);
        _todoServiceMock.Verify(x => x.UpdateTodoAsync(id, request), Times.Once);
    }

    [Fact(DisplayName = "Delete todo | success")]
    public async Task Should_ReturnNoContentResult_When_DeleteTodoItemWithExistingId()
    {
        var id = 1;
        _todoServiceMock.Setup(s => s.DeleteTodoAsync(id)).ReturnsAsync(true);
        var result = await _controller.DeleteTodoItem(id);
        Assert.IsType<NoContentResult>(result);
        _todoServiceMock.Verify(x => x.DeleteTodoAsync(id), Times.Once);
    }

    [Fact(DisplayName = "Delete todo | not found")]
    public async Task Should_ReturnNotFoundResult_When_DeleteTodoItemWithNonExistingId()
    {
        var id = 999;
        _todoServiceMock.Setup(s => s.DeleteTodoAsync(id)).ReturnsAsync(false);
        var result = await _controller.DeleteTodoItem(id);
        Assert.IsType<NotFoundResult>(result);
        _todoServiceMock.Verify(x => x.DeleteTodoAsync(id), Times.Once);
    }

    [Fact(DisplayName = "Update Todo Complete | Mark Completed")]
    public async Task Should_ReturnOk_When_TodoIsMarkedCompleted()
    {
        var response = new TodoResponse
        {
            Id = 1,
            Name = "Buy groceries",
            IsCompleted = true,
            CompletedAt = DateTime.UtcNow
        };

        _todoServiceMock
            .Setup(x => x.UpdateTodoCompleteAsync(1, true))
            .ReturnsAsync(response);

        var result = await _controller.UpdateTodoItemComplete(1, true);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(response, okResult.Value);

        _todoServiceMock.Verify(
            x => x.UpdateTodoCompleteAsync(1, true),
            Times.Once);
    }

    [Fact(DisplayName = "Update Todo Complete | Mark Incomplete")]
    public async Task Should_ReturnOk_When_TodoIsMarkedIncomplete()
    {
        var response = new TodoResponse
        {
            Id = 1,
            Name = "Buy groceries",
            IsCompleted = false,
            CompletedAt = null
        };

        _todoServiceMock
            .Setup(x => x.UpdateTodoCompleteAsync(1, false))
            .ReturnsAsync(response);

        var result = await _controller.UpdateTodoItemComplete(1, false);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(response, okResult.Value);

        _todoServiceMock.Verify(
            x => x.UpdateTodoCompleteAsync(1, false),
            Times.Once);
    }

    [Fact(DisplayName = "Update Todo Complete | Not Found")]
    public async Task Should_ReturnNotFound_When_TodoDoesNotExist()
    {
        _todoServiceMock
            .Setup(x => x.UpdateTodoCompleteAsync(100, true))
            .ReturnsAsync((TodoResponse?)null);

        var result = await _controller.UpdateTodoItemComplete(100, true);

        Assert.IsType<NotFoundResult>(result);

        _todoServiceMock.Verify(
            x => x.UpdateTodoCompleteAsync(100, true),
            Times.Once);
    }


}



