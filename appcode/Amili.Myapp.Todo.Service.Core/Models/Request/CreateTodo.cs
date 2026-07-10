using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amili.Myapp.Todo.Service.Core.Models.Request;

public class CreateTodo
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [StringLength(256)]
    public string? Description { get; set; }
}
