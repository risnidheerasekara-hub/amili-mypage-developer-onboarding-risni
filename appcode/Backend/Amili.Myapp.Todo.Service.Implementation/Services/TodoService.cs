using DataModels = Amili.Myapp.Todo.Service.Core.DataModels;
using Amili.Myapp.Todo.Service.Core.Models.Request;
using Amili.Myapp.Todo.Service.Core.Models.Response;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Amili.Myapp.Todo.Service.Implementation.Data;
using Amili.Myapp.Todo.Service.Core.Services;

namespace Amili.Myapp.Todo.Service.Implementation.Services;
public class TodoService(TodoDbContext dbcontext, IMapper mapper) : ITodoService
    {
    public async Task<TodoResponse> CreateTodoAsync(CreateTodoRequest request)
    {
        var todoItem = mapper.Map<DataModels.Todo>(request);

        dbcontext.Todos.Add(todoItem);
        await dbcontext.SaveChangesAsync();

        return mapper.Map<TodoResponse>(todoItem);
    }

    public async Task<TodoResponse?> GetTodoByIdAsync(long id)
    {
        var todoItem = await dbcontext.Todos.FindAsync(id);
        if (todoItem == null)
        {
            return null;
        }
        return mapper.Map<TodoResponse>(todoItem);
    }

    public async Task<TodoResponse[]> GetAllTodosAsync()
    {
        var todoItems = await dbcontext.Todos
            .OrderBy(t => t.CreatedAt)
            .ToListAsync();
        return mapper.Map<TodoResponse[]>(todoItems);
    }

    public async Task<TodoResponse?> UpdateTodoAsync(long id, UpdateTodoRequest request)
    {
        var todoItem = await dbcontext.Todos.FindAsync(id);
        if (todoItem == null)
        {
            return null;
        }

        mapper.Map(request, todoItem);
        await dbcontext.SaveChangesAsync();

        return mapper.Map<TodoResponse>(todoItem);
    }

    public async Task<bool> DeleteTodoAsync(long id)
    {
        var todoItem = await dbcontext.Todos.FindAsync(id);
        if (todoItem == null)
        {
            return false;
        }

        dbcontext.Todos.Remove(todoItem);
        await dbcontext.SaveChangesAsync();
        return true;
    }

    public async Task<TodoResponse?> UpdateTodoCompleteAsync(long id, bool isCompleted)
    {
        var todoItem = await dbcontext.Todos.FindAsync(id);
        if (todoItem == null)
        {
            return null;
        }

        if (isCompleted)
        {
            todoItem.IsCompleted = isCompleted;
            todoItem.CompletedAt = DateTime.UtcNow;
        }
        if (!isCompleted)
        {
            todoItem.IsCompleted = isCompleted;
            todoItem.CompletedAt = null;
        }

        return mapper.Map<TodoResponse>(todoItem);
    }
}

