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
    public async Task<TodoResponse> CreateTodoAsync(CreateTodo request)
    {
        var todoItem = mapper.Map<DataModels.Todo>(request);
        todoItem.CreatedAt = DateTime.UtcNow;

        dbcontext.TodoItems.Add(todoItem);
        await dbcontext.SaveChangesAsync();

        return mapper.Map<TodoResponse>(todoItem);
    }

    public async Task<TodoResponse> GetTodoByIdAsync(long id)
    {
        var todoItem = await dbcontext.TodoItems.FindAsync(id);
        if (todoItem == null)
        {
            return null;
        }
        return mapper.Map<TodoResponse>(todoItem);
    }

    public async Task<TodoResponse[]> GetAllTodosAsync()
    {
        var todoItems = await dbcontext.TodoItems
            .OrderBy(t => t.CreatedAt)
            .ToListAsync();
        return mapper.Map<TodoResponse[]>(todoItems);
    }

    public async Task<TodoResponse?> UpdateTodoAsync(long id, UpdateTodo request)
    {
        var todoItem = await dbcontext.TodoItems.FindAsync(id);
        if (todoItem == null)
        {
            return null;
        }

        if (request.Name != null)
        {
            todoItem.Name = request.Name;
        }
        if (request.Description != null)
        {
            todoItem.Description = request.Description;
        }
        if (request.IsCompleted == true)
        {
            todoItem.IsCompleted = request.IsCompleted.Value;
            todoItem.CompletedAt = DateTime.UtcNow;
        }
        if (request.IsCompleted == false)
        {
            todoItem.IsCompleted = request.IsCompleted.Value;
            todoItem.CompletedAt = null;
        }

        await dbcontext.SaveChangesAsync();

        return mapper.Map<TodoResponse>(todoItem);
    }

    public async Task<string?> DeleteTodoAsync(long id)
    {
        var todoItem = await dbcontext.TodoItems.FindAsync(id);
        if (todoItem == null)
        {
            return null;
        }

        dbcontext.TodoItems.Remove(todoItem);
        await dbcontext.SaveChangesAsync();

        return $"Todo item with ID {id} has been deleted.";
    }
}

