using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CatalogoAPI.Models;
using Microsoft.IdentityModel.Tokens;

namespace CatalogoAPI.Services
{
    public class TokenService : ITokenService
    {
        public string GeraToken(string key, string issuer, string audience, Usuario usuario)
        {
           var claims = new[]
           {
            new Claim(ClaimTypes.Name, usuario.NomeUsuario),
            new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
           };

           var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

           var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

           var token = new JwtSecurityToken(issuer: issuer, 
                                            audience: audience, 
                                            claims: claims, 
                                            expires: DateTime.Now.AddMinutes(10), 
                                            signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }
    }
}