namespace TodoAPI.Data;

public class Jwt
{
    public string Iss { get; set; } = string.Empty;
    public string Aud { get; set;  } = string.Empty;
    public string Key { get; set; } = string.Empty;
}

