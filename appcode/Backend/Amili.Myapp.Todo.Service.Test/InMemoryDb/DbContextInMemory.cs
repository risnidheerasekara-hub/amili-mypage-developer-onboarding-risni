using Amili.Myapp.Todo.Service.Implementation.Data;
using Microsoft.EntityFrameworkCore;

namespace Amili.Myapp.Todo.Service.Test.InMemoryDb;

public class TodoDbContextInMemory : TodoDbContext
{
    public TodoDbContextInMemory()
        : base(new DbContextOptionsBuilder<TodoDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options)
    {
    }
}
