using Chat_DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Chat_BL;

public class TokenService : ITokenService
{
    private readonly SymmetricSecurityKey _key;
    public TokenService(IConfiguration config)
    {
        _key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config["TokenKey"]));
    }
    public string CreateToken(ChatUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.NameId,user.UserName)
        };

        var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);

        var tokenDes = new SecurityTokenDescriptor 
        { 
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDes);

        return tokenHandler.WriteToken(token);
        
    }
}
