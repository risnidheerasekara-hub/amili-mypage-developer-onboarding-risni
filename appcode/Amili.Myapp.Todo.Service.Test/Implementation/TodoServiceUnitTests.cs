using Amili.Myapp.Todo.Service.Core.Models.Request;
using Amili.Myapp.Todo.Service.Implementation.Mapper;
using Amili.Myapp.Todo.Service.Implementation.Services;
using Amili.Myapp.Todo.Service.Test.InMemoryDb;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Datamodels = Amili.Myapp.Todo.Service.Core.DataModels;

namespace Amili.Myapp.Todo.Service.Test.Implementation;

public class TodoServiceUnitTests
{
    private readonly TodoDbContextInMemory _dbContext;
    private readonly TodoService _todoService;

    public TodoServiceUnitTests()
    {
        _dbContext = new TodoDbContextInMemory();

        var services = new ServiceCollection();
         services.AddLogging();
        services.AddAutoMapper(cfg => { }, typeof(MapperProfile));
        var mapper = services.BuildServiceProvider().GetRequiredService<IMapper>();

        _todoService = new TodoService(_dbContext, mapper);
    }

    [Fact(DisplayName = "Create Todo item")]
    public async Task Should_ReturnCreatedAtActionResult_When_CreateTodoItem_WithValidRequest()
    {
        var request = new CreateTodoRequest { Name = "Buy groceries", Description = "Milk, eggs, bread" };

        var result = await _todoService.CreateTodoAsync(request);

        Assert.NotNull(result);
        Assert.Equal(request.Name, result.Name);
        Assert.Equal(request.Description, result.Description);
        Assert.Single(_dbContext.Todos);
    }

    [Fact(DisplayName = "Get todo item with existing id")]
    public async Task Should_ReturnTodo_When_TodoExists()
    {
        var todo = new Datamodels.Todo
        {
            Name = "Test Todo",
            Description = "Test Description",
            CreatedAt = DateTime.UtcNow,
            IsCompleted = false,
            CompletedAt = null
        };

        _dbContext.Add(todo);
        await _dbContext.SaveChangesAsync();

        var result = await _todoService.GetTodoByIdAsync(todo.Id);

        Assert.NotNull(result);
        Assert.Equal(todo.Id, result.Id);
        Assert.Equal(todo.Name, result.Name);
        Assert.Equal(todo.Description, result.Description);
        Assert.Equal(todo.CreatedAt, result.CreatedAt);
        Assert.Equal(todo.IsCompleted, result.IsCompleted);
        Assert.Equal(todo.CompletedAt, result.CompletedAt);
    }



}
