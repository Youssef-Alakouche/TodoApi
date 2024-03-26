

namespace TodoAPI.Models;

public class TodoUpdateDTO
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool isComplete { get; set; }

    public TodoItem ToTodoItem(TodoItem todo)
    {
        todo.Description = Description;
        todo.IsComplete = isComplete;

        // Title is required, so null and white space no allowed.
        if (!string.IsNullOrWhiteSpace(Title))
            todo.Title = Title;

        return todo;
    }
}

