using Microsoft.EntityFrameworkCore;
using TodoAPI.Models;

namespace TodoAPI.Data;

public class SeedData
{
    public static void Seed(DataBaseContext context)
    {
        if (!context.TodoItems!.Any() && !context.Users.Any())
        {
            context.Database.Migrate();

            User user = new()
            {
                Name = "user 1",
                Email = "user1@gmail.com",
                Password = "pa$$word",
                RegisteredAt = DateTime.Now
            };

            
            // context.SaveChanges();
            
             List<TodoItem> todoItems = [
                new TodoItem { Title = "Complete assignment", Description = "Finish the task by Friday", CreateAt = DateTime.Now.AddDays(-2), IsComplete = false, user = user},
                new TodoItem { Title = "Buy groceries", CreateAt = DateTime.Now, IsComplete = true , user = user},
                new TodoItem { Title = "Call mom", CreateAt = DateTime.Now.AddDays(-1), IsComplete = false, user = user}
            ]; 
             
             context.AddRange(todoItems);

             context.SaveChanges();
        }
    }
}

