using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Amili.Myapp.Todo.Service.Core.DataModels;

[Table("Todo")]
public class Todo
{
    [Key]
    [Column(TypeName = "bigint")]
    public long Id { get; set; }

    [Column(TypeName = "varchar(255)")]
    [StringLength(255)]
    public string Name { get; set; } = string.Empty;

    [Column(TypeName = "text")]
    public string? Description { get; set; }

    [Column(TypeName = "timestamptz")]
    public DateTime CreatedAt { get; set; }

    public bool IsCompleted { get; set; }

    [Column(TypeName = "timestamptz")]
    public DateTime? CompletedAt { get; set; }
}
