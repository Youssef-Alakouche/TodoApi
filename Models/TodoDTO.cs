using System.ComponentModel.DataAnnotations;

namespace  TodoAPI.Models;

public class TodoDTO
{
    // you can use require keyword.
    [Required] public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }

    public TodoItem  ToTodoItem()
    {
        return new TodoItem()
        {
            Title = Title,
            Description = Description,
            IsComplete = false,
            CreateAt = DateTime.Now
        };
    }
}

