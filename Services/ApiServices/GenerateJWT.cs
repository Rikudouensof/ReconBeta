using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ReconBeta.Data;
using ReconBeta.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ReconBeta.Services.ApiServices
{
  public class GenerateJWT
  {
    private readonly SignInManager<User> _signInManager;
    private  IConfiguration _configuration;
    private readonly ApplicationDbContext _db;
    public GenerateJWT(SignInManager<User> signInManager, IUserService userService, IConfiguration configuration, ApplicationDbContext db)
    {
      _signInManager = signInManager;
      _configuration = configuration;
      _db = db;
    }
    public string GenerateJWTToken(User user)
    {
      var stringKey = Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]);

      var claims = new Claim[]
      {
        new Claim(ClaimTypes.Name, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.UserName)
      };

      var credentials = new SigningCredentials(new SymmetricSecurityKey(stringKey), SecurityAlgorithms.HmacSha256);


      JwtSecurityToken token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Issuer"], claims, notBefore: DateTime.Now, expires: DateTime.Now.AddDays(7), signingCredentials: credentials);
   

      return new JwtSecurityTokenHandler().WriteToken(token);
    }
  }
}
