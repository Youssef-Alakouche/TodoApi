
using Microsoft.EntityFrameworkCore;
using TodoAPI.Models;

namespace TodoAPI.Data;

public class DataBaseContext : DbContext
{
    public DataBaseContext(DbContextOptions<DataBaseContext> options): base(options){}
    
    public DbSet<TodoItem>? TodoItems { get; set; }
    public DbSet<User> Users { get; set; }


     protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed data here 
        // or create a separated extension method
    }
}

