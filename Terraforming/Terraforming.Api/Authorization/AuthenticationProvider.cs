using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Terraforming.Api.Models;

namespace Terraforming.Api.Authorization
{
    public interface IAuthenticationProvider
    {
        JwtSecurityToken CreateToken(User user);
    }

    public class AuthenticationProvider : IAuthenticationProvider
    {
        public readonly IConfiguration config;

        public AuthenticationProvider(IConfiguration _config)
        {
            config = _config;
        }

        public JwtSecurityToken CreateToken(User user)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim("Id", user.Id));
            claims.Add(new Claim("Lastname", user.Lastname));
            claims.Add(new Claim("Name", user.Firstname));
            claims.Add(new Claim("Email", user.Email));
            claims.Add(new Claim("Nickname", user.Nickname));
            claims.Add(new Claim("UserRole", ((int)user.UserRole).ToString()));
            if (user.UserRole == UserRole.Admin)
            {
                claims.Add(new Claim("Administrator", user.UserRole.ToString()));
                claims.Add(new Claim("GameMaster", user.UserRole.ToString()));
            }
            if (user.UserRole == UserRole.GM)
            {
                claims.Add(new Claim("GameMaster", user.UserRole.ToString()));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
              config["Tokens:Issuer"],
              config["Tokens:Issuer"],
              claims,
              expires: DateTime.UtcNow.AddMinutes(560),
              signingCredentials: creds);

            return token;
        }



    }
}
