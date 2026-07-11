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
    }
}
