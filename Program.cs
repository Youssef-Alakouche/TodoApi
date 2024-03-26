using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TodoAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Entityframework core severvice
builder.Services.AddDbContext<DataBaseContext>(o =>
{
    o.UseSqlServer(
            builder.Configuration.GetConnectionString("sqlServer")
        );
});

builder.Services.Configure<Jwt>(
    builder.Configuration.GetSection("Jwt")
    );

// Authentication Service
builder.Services.AddAuthentication()
    .AddJwtBearer(o =>
    {
        Jwt jwtO = builder.Configuration.GetSection("Jwt").Get<Jwt>()!;

        o.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidIssuer = jwtO.Iss,
            
            ValidateAudience = true,
            ValidAudience = jwtO.Aud,
            
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(jwtO.Key))
                
        };
    });

builder.Services.AddControllers();
var app = builder.Build();


app.MapGet("/", () =>
{
    Jwt jwtO = app.Configuration.GetSection("Jwt").Get<Jwt>()!;
});

// app.UseHttpsRedirection();

app.MapControllers();


// Init Seed DataBase
var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<DataBaseContext>();
SeedData.Seed(context);

app.Run();

