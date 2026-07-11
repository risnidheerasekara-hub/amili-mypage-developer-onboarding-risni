using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amili.Myapp.Todo.Service.Core.Models.Request;

public class UpdateTodoRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}

