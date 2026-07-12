using Amili.Myapp.Todo.Service.Implementation.Data;
using Microsoft.EntityFrameworkCore;
using Amili.Myapp.Todo.Service.Core.Services;
using Amili.Myapp.Todo.Service.Implementation.Services;
using Amili.Myapp.Todo.Service.Implementation.Mapper;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(cfg => { }, typeof(MapperProfile));
builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddDbContext<TodoDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    );
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactDev", policy =>
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod());
});


var app = builder.Build();
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseCors("AllowReactDev");

app.UseAuthorization();

app.MapControllers();

app.Run();
