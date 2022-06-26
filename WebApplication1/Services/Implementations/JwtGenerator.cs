using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using WebApplication1.Services.Interfaces;
using WebApplication1.Data.Entities;

namespace WebApplication1.Services.Implementations
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly IConfiguration _config;
        public JwtGenerator(IConfiguration config)
        {
            _config=config;
        }
        public string GenerateJwt(AppUser user, IList<string> roles)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("FullName",user.FullName),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Name,user.UserName)
            };
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)).ToList());



            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("Jwt:secretKey").Value));

            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                expires: DateTime.Now.AddDays(3),
                claims: claims,
                signingCredentials: credentials,
                issuer: _config.GetSection("Jwt:issuer").Value,
                audience: _config.GetSection("Jwt:audience").Value

                );
            string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return token;
        }
    }
}
