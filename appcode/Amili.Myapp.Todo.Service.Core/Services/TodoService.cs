using Amili.Myapp.Todo.Service.Core.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amili.Myapp.Todo.Service.Core.DataModels;
using Amili.Myapp.Todo.Service.Core.Models.Request;
namespace Amili.Myapp.Todo.Service.Core.Services;

public interface ITodoService
{
    Task<TodoResponse> CreateTodoAsync(Models.Request.CreateTodo request);
    Task<TodoResponse> GetTodoByIdAsync(long id);
    Task<TodoResponse[]> GetAllTodosAsync();
    Task<TodoResponse?> UpdateTodoAsync(long id, Models.Request.UpdateTodo request);
    Task<string?> DeleteTodoAsync(long id);
}
