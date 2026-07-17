using Amili.Myapp.Todo.Service.Core.Models.Request;
using Amili.Myapp.Todo.Service.Implementation.Mapper;
using Amili.Myapp.Todo.Service.Implementation.Services;
using Amili.Myapp.Todo.Service.Test.InMemoryDb;
using AutoMapper;
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
        //Arrange
        var request = new CreateTodoRequest { Name = "Buy groceries", Description = "Milk, eggs, bread" };

        //Act
        var result = await _todoService.CreateTodoAsync(request);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(request.Name, result.Name);
        Assert.Equal(request.Description, result.Description);
        Assert.Single(_dbContext.Todos);
    }

    [Fact(DisplayName = "Get todo item with existing id")]
    public async Task Should_ReturnTodo_When_TodoExists()
    {
        // Arrange
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

        //Act
        var result = await _todoService.GetTodoByIdAsync(todo.Id);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(todo.Id, result.Id);
        Assert.Equal(todo.Name, result.Name);
        Assert.Equal(todo.Description, result.Description);
        Assert.Equal(todo.CreatedAt, result.CreatedAt);
        Assert.Equal(todo.IsCompleted, result.IsCompleted);
        Assert.Equal(todo.CompletedAt, result.CompletedAt);
    }
    [Fact(DisplayName = "Get todo item with non existing id")]
    public async Task Should_ReturnNull_When_TodoDoesNotExist()
    {
        // Arrange
        var id = 999;

        //Act
        var result = await _todoService.GetTodoByIdAsync(id);

        //Assert
        Assert.Null(result);
    }

    [Fact(DisplayName = "Update todo item with existing id")]
    public async Task Should_UpdateAndReturnTodo_When_UpdateTodoAsync_IsInvokedWithExistingIdAndValidRequest()
    {
        // Arrange
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
        var updateRequest = new UpdateTodoRequest
        {
            Name = "Updated Todo",
            Description = "Updated Description"
        };

        // Act
        var result = await _todoService.UpdateTodoAsync(todo.Id, updateRequest);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(todo.Id, result.Id);
        Assert.Equal(updateRequest.Name, result.Name);
        Assert.Equal(updateRequest.Description, result.Description);
    }

    [Fact(DisplayName = "Update todo item with non existing id")]
    public async Task Should_ReturnNull_When_UpdateTodoAsync_IsInvokedWithNonExistingId()
    {
        // Arrange
        var updateRequest = new UpdateTodoRequest
        {
            Name = "Updated Todo",
            Description = "Updated Description"
        };

        // Act
        var result = await _todoService.UpdateTodoAsync(999, updateRequest);

        //Assert
        Assert.Null(result);
    }

    [Fact(DisplayName = "Update todo item with null description")]
    public async Task Should_KeepExistingDescription_When_UpdateTodoAsync_IsInvokedWithNullDescription()
    {
        // Arrange
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
        var updateRequest = new UpdateTodoRequest
        {
            Name = "Updated Todo",
            Description = null
        };

        // Act
        var result = await _todoService.UpdateTodoAsync(todo.Id, updateRequest);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(todo.Id, result.Id);
        Assert.Equal(updateRequest.Name, result.Name);
        Assert.Equal(todo.Description, result.Description);
    }

    [Fact(DisplayName = "Delete todo item with existing id")]
    public async Task Should_ReturnTrueAndRemoveTodo_When_DeleteTodoAsync_IsInvokedWithExistingId()
    {

        // Arrange
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

        //Act
        var result = await _todoService.DeleteTodoAsync(todo.Id);

        //Assert
        Assert.True(result);
    }

    [Fact(DisplayName = "Delete todo item with non existing id")]
    public async Task Should_ReturnFalse_When_DeleteTodoAsync_IsInvokedWithNonExistingId()
    {
        //Arrange
        var id = 999;

        //Act
        var result = await _todoService.DeleteTodoAsync(id);

        //Assert
        Assert.False(result);
    }

    [Fact(DisplayName = "Update todo item complete with existing id")]
    public async Task Should_SetIsCompletedTrueAndCompletedAt_When_UpdateTodoCompleteAsync_IsInvoked()
    {
        //Arrange
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

        //Act
        var result = await _todoService.CompleteTodoAsync(todo.Id);

        //Assert
        Assert.NotNull(result);
        Assert.True(result.IsCompleted);
        Assert.NotNull(result.CompletedAt);
    }

    [Fact(DisplayName = "Update todo item complete with non existing id")]
    public async Task Should_ReturnNull_When_UpdateTodoCompleteAsync_IsInvokedWithNonExistingId()
    {
        //Arrange
        var id = 999;

        //Act
        var result = await _todoService.CompleteTodoAsync(id);

        //Assert
        Assert.Null(result);
    }

    [Fact(DisplayName = "Update todo item complete should persist change to database")]
    public async Task Should_PersistChangeToDatabase_When_UpdateTodoCompleteAsync_IsInvoked()
    {

        //Arrange
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

        //Act
        await _todoService.CompleteTodoAsync(todo.Id);
        var updatedTodo = await _dbContext.Todos.FindAsync(todo.Id);

        //Assert
        Assert.NotNull(updatedTodo);
        Assert.True(updatedTodo.IsCompleted);
        Assert.NotNull(updatedTodo.CompletedAt);
    }



}
