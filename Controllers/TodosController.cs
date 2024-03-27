
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoAPI.Data;
using TodoAPI.Models;

namespace TodoAPI.Controllers;

[Authorize]
[ApiController]
[Route("/[Controller]")]
public class TodosController : ControllerBase
{
    private readonly DataBaseContext _context;
    // user id
    private readonly int _id;

    public TodosController(DataBaseContext context, IHttpContextAccessor contextAccessor)
    {
        HttpContext httpContext = contextAccessor.HttpContext!;
        _context = context;
        try
        {
            _id = int.Parse(httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid)?.Value!);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    
    // GET /todos
    [HttpGet]
    public ActionResult<IEnumerable<TodoItem>> TodoItems()
    {
        // var items = _context.TodoItems?.AsEnumerable() ?? Enumerable.Empty<TodoItem>();
        Console.WriteLine(_id);
        var items = _context.Users.First(u => u.Id == _id);
        _context.Entry(items).Collection(u => u.Todos!).Load();

        if (items.Todos is not null)
        {
            foreach (var todo in items.Todos)
            {
                todo.user = null;
            }
        }
        
        return Ok(items.Todos);
    }
    
    // GET /todos/{id}

    [HttpGet("{id}")]
    public ActionResult<TodoItem> TodoItem(long id)
    {
        var user = _context.Users.First(u => u.Id == _id);

        var item = _context.Entry(user).Collection(u => u.Todos!).Query().FirstOrDefault(t => t.Id == id);

        
        
        // var item = _context.TodoItems?.FirstOrDefault(i => i.Id == id);

        if (item is null)
        {
            return BadRequest($"Todo with id {id} doesn't exist.");
        }
        item.user = null;
        
        return Ok(item);
    }
    
    // GET /todos/complete
    [HttpGet]
    [Route("complete")]
    public ActionResult<IEnumerable<TodoItem>> TodoItemsComplete()
    {
        var user = _context.Users.First(u => u.Id == _id);

        var completeTodos = _context.Entry(user).Collection(u => u.Todos!).Query().Where(i => i.IsComplete);
        
        // var completeTodos = _context.TodoItems!.Where(i => i.IsComplete);

        if (!completeTodos.Any())
            return BadRequest("No Todos has been completed!");
    
        
        foreach (var todo in completeTodos)
        {
            todo.user = null;
        }
        
        
        return Ok(completeTodos);
    }
    
    // POST /todos
    [HttpPost]
    public ActionResult<TodoItem> PostTodoItem(TodoDTO todoItem)
    {
        var newTodo = todoItem.ToTodoItem();

        // assign TodoItem to the current user
        newTodo.UserId = _id;
        
        // suppress deplicate Title
        if (_context.TodoItems!.Any(i => i.Title == newTodo.Title))
            return BadRequest("Deplicate Todo Title. Choose another Title.");
        
        var entity =  _context.Add(newTodo);
        
        _context.SaveChanges();
        
        return Ok(entity.Entity);
    }
    
    
    // PUT /todos/{id}
    [HttpPut("{id}")]
    public ActionResult<TodoItem> TodoItemUpdate(long id, TodoUpdateDTO updatedTodo)
    {
        var todo = _context.TodoItems?.FirstOrDefault(i => i.Id == id);
        if (todo is null)
            return BadRequest($"No Todo with id of {id}");

        // suppress update to an existance Todo with the same Title
        if (todo.Title != updatedTodo.Title && _context.TodoItems!.Any(i => 
                EF.Functions.Collate(i.Title, "Latin1_General_CI_AS").Equals(updatedTodo.Title)))
            return BadRequest("Deplicate Todo Title");
        
        

        todo = updatedTodo.ToTodoItem(todo);

        // assign todoItem to the current user.
        todo.UserId = _id;
        _context.Update(todo);

        _context.SaveChanges();

        return Ok(todo);
    }
    
    
    // Delete /todos/{id}
    [HttpDelete("{id}")]
    public ActionResult TodoItemDelete(long id)
    {
        var todo = _context.TodoItems?.FirstOrDefault(i => i.Id == id);
        if (todo is null)
            return BadRequest($"There is no Todo with id {id}");

        _context.Remove(todo);
        _context.SaveChanges();

        return Ok($"Todo {id} removed.");
    }
}

