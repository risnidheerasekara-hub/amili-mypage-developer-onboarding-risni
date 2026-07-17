using Amili.Myapp.Todo.Service.Core.Models.Request;
using Amili.Myapp.Todo.Service.Core.Models.Response;

namespace Amili.Myapp.Todo.Service.Core.Services;

public interface ITodoService
{
    Task<TodoResponse> CreateTodoAsync(CreateTodoRequest request);
    Task<TodoResponse?> GetTodoByIdAsync(long id);
    Task<TodoResponse[]> GetAllTodosAsync();
    Task<TodoResponse?> UpdateTodoAsync(long id, UpdateTodoRequest request);
    Task<bool> DeleteTodoAsync(long id);
    Task<TodoResponse?> CompleteTodoAsync(long id);
}
