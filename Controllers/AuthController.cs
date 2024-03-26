using Microsoft.AspNetCore.Mvc;

using TodoAPI.Data;
using TodoAPI.Utility;

namespace TodoAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost]
    [Route("login")]
    public ActionResult login(UserLogin cred, IServiceProvider serviceProvider)
    {
        /*
         * check email format validation
         * check for valid from database
         */
        
        // login for test
        if (string.IsNullOrWhiteSpace(cred.Email) || string.IsNullOrWhiteSpace(cred.Password))
            return BadRequest();

        // get user from db

        
        var token = ActivatorUtilities.CreateInstance<UserTokenGeneration>(serviceProvider).UserTokenGeneratorHandler(new User()
        {
            Name = "user",
            Email = cred.Email,
            RegisteredAt = DateTime.Now
        });

        return Ok(token);

    }
}

