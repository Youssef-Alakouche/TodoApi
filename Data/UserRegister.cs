using System.ComponentModel.DataAnnotations;

namespace  TodoAPI.Data;

public class UserRegister
{
    
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public string Email { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;
}

