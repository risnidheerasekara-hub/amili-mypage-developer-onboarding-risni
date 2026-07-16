using Amili.Myapp.Todo.Service.Core.Models.Request;
using Amili.Myapp.Todo.Service.Core.Models.Response;
using Amili.Myapp.Todo.Service.Core.Services;
using Amili.Myapp.Todo.Service.Implementation.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using DataModels = Amili.Myapp.Todo.Service.Core.DataModels;

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

        if (!string.IsNullOrEmpty(request.Name))
        {
            todoItem.Name = request.Name;
        }
        if (!string.IsNullOrEmpty(request.Description))
        {
            todoItem.Description = request.Description;
        }

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
        var rowsAffected = await dbcontext.SaveChangesAsync();
        return rowsAffected > 0;
    }

    public async Task<TodoResponse?> CompleteTodoAsync(long id)
    {
        var todoItem = await dbcontext.Todos.FindAsync(id);
        if (todoItem == null)
        {
            return null;
        }

        todoItem.IsCompleted = true;
        todoItem.CompletedAt = DateTime.UtcNow;

        await dbcontext.SaveChangesAsync();

        await dbcontext.SaveChangesAsync();

        return mapper.Map<TodoResponse>(todoItem);
    }
}

