

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TodoAPI.Data;
using TodoAPI.Models;

namespace TodoAPI.Utility;

public class UserTokenGeneration
{
    private  readonly Jwt _jwtO;

    public UserTokenGeneration(IOptions<Jwt> jwtO)
    {
        _jwtO = jwtO.Value;
    }

    public  string UserTokenGeneratorHandler(User user)
    {
        ClaimsIdentity identity = new([
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Sid, user.Id.ToString())
        ]);

        SecurityKey key = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(_jwtO.Key));

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = identity,
            Issuer = _jwtO.Iss,
            Audience = _jwtO.Aud,
            
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var accessToken = tokenHandler.CreateToken(tokenDescriptor);
        
        return tokenHandler.WriteToken(accessToken);
    }
}



