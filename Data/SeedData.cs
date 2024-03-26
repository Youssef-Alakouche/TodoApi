using Microsoft.EntityFrameworkCore;
using TodoAPI.Models;

namespace TodoAPI.Data;

public class SeedData
{
    public static void Seed(DataBaseContext context)
    {
        if (!context.TodoItems!.Any())
        {
            context.Database.Migrate();
            
             List<TodoItem> todoItems = [
                new TodoItem { Title = "Complete assignment", Description = "Finish the task by Friday", CreateAt = DateTime.Now.AddDays(-2), IsComplete = false },
                new TodoItem { Title = "Buy groceries", CreateAt = DateTime.Now, IsComplete = true },
                new TodoItem { Title = "Call mom", CreateAt = DateTime.Now.AddDays(-1), IsComplete = false }
            ]; 
             
             context.AddRange(todoItems);

             context.SaveChanges();
        }
    }
}

