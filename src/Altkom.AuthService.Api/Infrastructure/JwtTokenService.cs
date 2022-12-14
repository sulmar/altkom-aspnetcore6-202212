using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Altkom.AuthService.Api.Domain;
using Altkom.Shopper.Domain;
using Microsoft.IdentityModel.Tokens;

namespace Altkom.AuthService.Api.Infrastructure;

// dotnet add package System.IdentityModel.Tokens.Jwt
public class JwtTokenService : ITokenService
{
    public string Create(User user)
    {
        string secretKey = "your-256-bit-secret";
        string issuer = "authapi.altkom.pl";
        string audience = "domain.com";

        var claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.ToShortDateString()),
            new Claim(ClaimTypes.Role, "developer" ),
            new Claim(ClaimTypes.Role, "trainer" ),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var securityToken = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials,
            claims: claims
        );

        string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

        return token;
    }
}
