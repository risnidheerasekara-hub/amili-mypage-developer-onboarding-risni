using Amili.Myapp.Todo.Service.Core.Services;
using Amili.Myapp.Todo.Service.Implementation.Data;
using Amili.Myapp.Todo.Service.Implementation.Mapper;
using Amili.Myapp.Todo.Service.Implementation.Services;
using Microsoft.EntityFrameworkCore;

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
        policy.WithOrigins("http://localhost:5173",
        "https://lemon-hill-0ea75cb03.7.azurestaticapps.net")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();
// Configure the HTTP request pipeline.
using var services = app.Services.CreateScope();
var dbContext = services.ServiceProvider.GetRequiredService<TodoDbContext>();
dbContext.Database.Migrate();


app.UseCors("AllowReactDev");

app.UseAuthorization();

app.MapControllers();

app.Run();
