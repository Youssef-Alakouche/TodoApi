using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoAPI.Data;
using TodoAPI.Models;
using TodoAPI.Utility;

namespace TodoAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly DataBaseContext _context;

    public AuthController(DataBaseContext context)
    {
        _context = context;
    }
    
    [HttpPost]
    [Route("login")]
    public ActionResult Login(UserLogin cred, IServiceProvider serviceProvider)
    {
        // login
        
        // check the validty of email and password
        if (!UserCredValidator.SimpleCredValidation(cred.Email, cred.Password))
        {
            return BadRequest("credentials is not valid!");
        }

        // get the user by email
        User? user = _context.Users.FirstOrDefault(u => u.Email == cred.Email);

        // no user with such email
        if (user is null)
            return BadRequest("There is no such user!");
        
        // user password validity check
        if (user.Password != cred.Password)
            return BadRequest("Password is Not Valid!");
        
    
        // login done

        // handler and Generate token
        var token = ActivatorUtilities.CreateInstance<UserTokenGeneration>(serviceProvider).UserTokenGeneratorHandler(user);

        return Ok(token);

    }

    [HttpPost()]
    [Route("register")]
    public ActionResult Regiser(User user)
    {
        if (!UserCredValidator.IsValidPassword(user.Password))
            return BadRequest("password is Empty");

        if (!UserCredValidator.IsValidName(user.Name))
            return BadRequest("Name is Empty");
        
        if (!UserCredValidator.IsValidEmail(user.Email))
        {
            return BadRequest("Email is not Valid");
        }
        
        // check for deplicate email id db
        if (_context.Users.Any(u =>
                EF.Functions.Collate(u.Email, "Latin1_General_CI_AS").Equals(user.Email)))
        {
            return BadRequest("Email is already exist!");
        }
        
        // register
        user.RegisteredAt = DateTime.Now;
        _context.Add(user);
        _context.SaveChanges();
     
        return Ok("Registeration done!");
    }
}

