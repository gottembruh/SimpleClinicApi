using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace SimpleClinicApi.Infrastructure.Auth.Utilities
{
   public class JwtTokenGenerator(IConfiguration configuration) : IJwtTokenGenerator
   {
      public string GenerateJwtToken(IdentityUser user)
      {
         var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
         var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

         var claims = new[]
         {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
         };

         var token = new JwtSecurityToken(
                                          issuer: configuration["Jwt:Issuer"],
                                          audience: configuration["Jwt:Audience"],
                                          claims: claims,
                                          expires:
                                          DateTime.UtcNow.AddHours(Convert.ToInt32(configuration["Jwt:Expires"])),
                                          signingCredentials: creds);

         return new JwtSecurityTokenHandler().WriteToken(token);
      }
   }
}