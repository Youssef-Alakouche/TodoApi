
using Microsoft.EntityFrameworkCore;
using TodoAPI.Models;

namespace TodoAPI.Data;

public class DataBaseContext : DbContext
{
    public DataBaseContext(DbContextOptions<DataBaseContext> options): base(options){}
    
    public DbSet<TodoItem>? TodoItems { get; set; }
    public DbSet<User> Users { get; set; }
}

