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
        //Arrange
        var request = new CreateTodoRequest { Name = "Buy groceries" };
        var response = new TodoResponse { Id = 1, Name = request.Name };
        _todoServiceMock.Setup(s => s.CreateTodoAsync(request)).ReturnsAsync(response);

        //Act
        var result = await _controller.CreateTodoItem(request);

        //Assert
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
        //Arrange
        var id = 1;
        var response = new TodoResponse { Id = id, Name = "Buy groceries" };
        _todoServiceMock.Setup(s => s.GetTodoByIdAsync(id)).ReturnsAsync(response);

        //Act
        var result = await _controller.GetTodoItemById(id);

        //Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(response, okResult.Value);
        _todoServiceMock.Verify(
            x => x.GetTodoByIdAsync(id),
            Times.Once);
    }

    [Fact(DisplayName = "Get todo by id | not found")]
    public async Task Should_ReturnNotFoundResult_When_GetTodoItemById_WithNonExistingId()
    {
        //Arrange
        var id = 999;
        _todoServiceMock.Setup(s => s.GetTodoByIdAsync(id)).ReturnsAsync((TodoResponse?)null);

        //Act
        var result = await _controller.GetTodoItemById(id);

        //Assert
        Assert.IsType<NotFoundResult>(result);
        _todoServiceMock.Verify(
            x => x.GetTodoByIdAsync(id),
            Times.Once);
    }

    [Fact(DisplayName = "Get all todos | success")]
    public async Task Should_ReturnOkObjectResultWithAllTodos_When_GetAllTodoItemsIsInvoked()
    {
        //Arrange
        var todos = new List<TodoResponse>
        {
            new TodoResponse { Id = 1, Name = "Buy groceries" },
            new TodoResponse { Id = 2, Name = "Walk the dog" }
        };
        _todoServiceMock.Setup(s => s.GetAllTodosAsync()).ReturnsAsync(todos.ToArray());

        //Act
        var result = await _controller.GetAllTodoItems();

        //Assert
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
        // Arrange
        _todoServiceMock.Setup(s => s.GetAllTodosAsync()).ReturnsAsync(Array.Empty<TodoResponse>());

        // Act
        var result = await _controller.GetAllTodoItems();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedTodos = Assert.IsAssignableFrom<IEnumerable<TodoResponse>>(okResult.Value);
        Assert.Empty(returnedTodos);
        _todoServiceMock.Verify(x => x.GetAllTodosAsync(), Times.Once);

    }

    [Fact(DisplayName = "Update todo | success")]
    public async Task Should_ReturnOkObjectResult_When_UpdateTodoItemWithExistingId()
    {
        //Arrange
        var id = 1;
        var request = new UpdateTodoRequest { Name = "Buy groceries and cook dinner" };
        var response = new TodoResponse { Id = id, Name = request.Name };
        _todoServiceMock.Setup(s => s.UpdateTodoAsync(id, request)).ReturnsAsync(response);

        //Act
        var result = await _controller.UpdateTodoItem(id, request);

        //Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(response, okResult.Value);
        _todoServiceMock.Verify(x => x.UpdateTodoAsync(id, request), Times.Once);
    }

    [Fact(DisplayName = "Update todo | not found")]
    public async Task Should_ReturnNotFoundResult_When_UpdateTodoItemWithNonExistingId()
    {
        //Arrange
        var id = 999;
        var request = new UpdateTodoRequest { Name = "Non-existing todo" };
        _todoServiceMock.Setup(s => s.UpdateTodoAsync(id, request)).ReturnsAsync((TodoResponse?)null);

        //Act
        var result = await _controller.UpdateTodoItem(id, request);

        //Assert
        Assert.IsType<NotFoundResult>(result);
        _todoServiceMock.Verify(x => x.UpdateTodoAsync(id, request), Times.Once);
    }

    [Fact(DisplayName = "Delete todo | success")]
    public async Task Should_ReturnNoContentResult_When_DeleteTodoItemWithExistingId()
    {
        //Arrange
        var id = 1;
        _todoServiceMock.Setup(s => s.DeleteTodoAsync(id)).ReturnsAsync(true);

        //Act
        var result = await _controller.DeleteTodoItem(id);

        //Assert
        Assert.IsType<NoContentResult>(result);
        _todoServiceMock.Verify(x => x.DeleteTodoAsync(id), Times.Once);
    }

    [Fact(DisplayName = "Delete todo | not found")]
    public async Task Should_ReturnNotFoundResult_When_DeleteTodoItemWithNonExistingId()
    {
        //Arrange
        var id = 999;
        _todoServiceMock.Setup(s => s.DeleteTodoAsync(id)).ReturnsAsync(false);

        //Act
        var result = await _controller.DeleteTodoItem(id);

        //Assert
        Assert.IsType<NotFoundResult>(result);
        _todoServiceMock.Verify(x => x.DeleteTodoAsync(id), Times.Once);
    }

    [Fact(DisplayName = "Update Todo Complete | Mark Completed")]
    public async Task Should_ReturnOk_When_TodoIsMarkedCompleted()
    {
        // Arrange
        var response = new TodoResponse
        {
            Id = 1,
            Name = "Buy groceries",
            IsCompleted = true,
            CompletedAt = DateTime.UtcNow
        };

        _todoServiceMock
            .Setup(x => x.CompleteTodoAsync(1))
            .ReturnsAsync(response);

        //Act
        var result = await _controller.CompleteTodoAsync(1);

        //Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(response, okResult.Value);

        _todoServiceMock.Verify(
            x => x.CompleteTodoAsync(1),
            Times.Once);
    }

    [Fact(DisplayName = "Update Todo Complete | Not Found")]
    public async Task Should_ReturnNotFound_When_TodoDoesNotExist()
    {
        // Arrange
        _todoServiceMock
            .Setup(x => x.CompleteTodoAsync(100))
            .ReturnsAsync((TodoResponse?)null);

        //Act
        var result = await _controller.CompleteTodoAsync(100);

        //Assert
        Assert.IsType<NotFoundResult>(result);

        _todoServiceMock.Verify(
            x => x.CompleteTodoAsync(100),
            Times.Once);
    }


}



