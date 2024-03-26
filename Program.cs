using Microsoft.EntityFrameworkCore;
using TodoAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Entityframework core severvice
builder.Services.AddDbContext<DataBaseContext>(o =>
{
    o.UseSqlServer(
            builder.Configuration.GetConnectionString("sqlServer")
        );
});

builder.Services.AddControllers();
var app = builder.Build();


// app.UseHttpsRedirection();

app.MapControllers();


// Init Seed DataBase
var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<DataBaseContext>();
SeedData.Seed(context);

app.Run();

