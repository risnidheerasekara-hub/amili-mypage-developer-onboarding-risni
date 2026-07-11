using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amili.Myapp.Todo.Service.Core.Models.Request;

public class CreateTodoRequest
{
    [Required]
    [StringLength(100)]
    public required string Name { get; set; } = default!;

    [StringLength(256)]
    public string? Description { get; set; }
}
