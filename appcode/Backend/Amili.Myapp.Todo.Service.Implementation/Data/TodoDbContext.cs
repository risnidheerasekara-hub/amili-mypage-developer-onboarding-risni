using Microsoft.EntityFrameworkCore;
using DataModels = Amili.Myapp.Todo.Service.Core.DataModels;

namespace Amili.Myapp.Todo.Service.Implementation.Data;

public class TodoDbContext(DbContextOptions<TodoDbContext> options) : DbContext(options)
{
    public DbSet<DataModels.Todo> Todos { get; set; }
}
