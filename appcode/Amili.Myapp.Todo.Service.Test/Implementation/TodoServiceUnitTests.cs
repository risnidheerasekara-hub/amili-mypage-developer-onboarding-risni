using Amili.Myapp.Todo.Service.Core.Models.Request;
using Amili.Myapp.Todo.Service.Implementation.Data;
using Amili.Myapp.Todo.Service.Implementation.Mapper;
using Amili.Myapp.Todo.Service.Implementation.Services;
using Amili.Myapp.Todo.Service.Test.InMemoryDb;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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

    [Fact(DisplayName = "Create Todo | Success")]
    public async Task Should_ReturnCreatedAtActionResult_When_CreateTodoItem_WithValidRequest()
    {
        var request = new CreateTodoRequest { Name = "Buy groceries", Description = "Milk, eggs, bread" };

        var result = await _todoService.CreateTodoAsync(request);

        Assert.NotNull(result);
        Assert.Equal(request.Name, result.Name);
        Assert.Equal(request.Description, result.Description);
        Assert.Single(_dbContext.Todos);
    }
}
