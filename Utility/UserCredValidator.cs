using System.Text.RegularExpressions;

namespace TodoAPI.Utility;

public static class UserCredValidator
{
    public static bool IsValidName(string? name)
    {
        return !string.IsNullOrWhiteSpace(name);
    }
    
    public static bool IsValidPassword(string? password)
    {
        return !string.IsNullOrWhiteSpace(password);
    }
    
    public static bool IsValidEmail(string? email)
    {
        return (!string.IsNullOrWhiteSpace(email) && Regex.IsMatch(email,
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
    }
    
    // simple login validation
    public static bool SimpleCredValidation(string? email, string? password)
    {
        return IsValidEmail(email) && IsValidPassword(password);


    }
}