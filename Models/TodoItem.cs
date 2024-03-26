using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TodoAPI.Models;

public class TodoItem
{
    [Required]
    public long Id { get; set; }

    [Required] public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreateAt { get; set; }
    public bool IsComplete { get; set; } = false;
}

