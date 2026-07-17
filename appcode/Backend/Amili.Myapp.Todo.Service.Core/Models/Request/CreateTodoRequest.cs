using System.ComponentModel.DataAnnotations;

namespace Amili.Myapp.Todo.Service.Core.Models.Request;

public class CreateTodoRequest
{
    [Required]
    [StringLength(100)]
    public required string Name { get; set; }

    [StringLength(256)]
    public string? Description { get; set; }
}
