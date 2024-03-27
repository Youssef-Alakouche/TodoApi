
using System.ComponentModel.DataAnnotations;

namespace TodoAPI.Models;

public class User
{
    public long Id { get; set; }
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public string Email { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;
    public DateTime? RegisteredAt { get; set; }
    
    // relationships
    public ICollection<TodoItem>? Todos { get; set; }

    public object Include(Func<object, object> func)
    {
        throw new NotImplementedException();
    }
}

