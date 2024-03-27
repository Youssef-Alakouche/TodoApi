using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TodoAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// httpcontext accessor
builder.Services.AddHttpContextAccessor();

// Entityframework core severvice
builder.Services.AddDbContext<DataBaseContext>(o =>
{
    o.UseSqlServer(
            builder.Configuration.GetConnectionString("sqlServer")
        );
});


builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.Configure<Jwt>(
    builder.Configuration.GetSection("Jwt")
    );

// Authentication Service
builder.Services.AddAuthentication()
    .AddJwtBearer(o =>
    {
        Jwt jwtO = builder.Configuration.GetSection("Jwt").Get<Jwt>()!;

        o.SaveToken = true;
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


app.MapGet("/", (HttpContext context) =>
{
    Jwt jwtO = app.Configuration.GetSection("Jwt").Get<Jwt>()!;

    var claims = context.User.Claims;

    Console.WriteLine(claims.Where(c => c.Type == ClaimTypes.Sid).FirstOrDefault());
    
});

// app.UseHttpsRedirection();

app.MapControllers();


// Init Seed DataBase
var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<DataBaseContext>();
SeedData.Seed(context);

app.Run();

