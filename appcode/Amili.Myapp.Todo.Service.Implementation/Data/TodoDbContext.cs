using DataModels = Amili.Myapp.Todo.Service.Core.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Amili.Myapp.Todo.Service.Implementation.Data;

public class TodoDbContext(DbContextOptions<TodoDbContext> options) : DbContext(options)
{
    public DbSet<DataModels.Todo> TodoItems { get; set; }
}
