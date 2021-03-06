using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ReconBeta.ApiClasses.Models;
using ReconBeta.Data;
using ReconBeta.Models;
using ReconBeta.Services.ApiServices;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ReconBeta.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ApiAuthController : ControllerBase
  {
    
    private IUserService _userService;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _db;
    public GenerateJWT generateJWT;
    public User userp { get; set; }
    public ApiAuthController(SignInManager<User> signInManager, IUserService userService, IConfiguration configuration, ApplicationDbContext db)
    {
      _signInManager = signInManager;
      _userService = userService;
      _configuration = configuration;
      _db = db;
    }

    // /api/Authentication/Register
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] ApiRegister model)
    {

      if (ModelState.IsValid)
      {
        var result = await _userService.RegisterUserAsync(model);

        if (result.IsSuccess)
        {
          return Ok(result);
        }
      }

      return BadRequest("Some properties are not valid");

    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login ([FromBody] ApiLogin apiLogin)
    {
      if (ModelState.IsValid)
      {
        // This doesn't count login failures towards account lockout
        // To enable password failures to trigger account lockout, set lockoutOnFailure: true
        var result = await _signInManager.PasswordSignInAsync(apiLogin.Email, apiLogin.Password, apiLogin.RememberMe, lockoutOnFailure: false);
        if (result.Succeeded)
        {
          var user = _db.Users.Where(m => m.Email == apiLogin.Email).FirstOrDefault();
          userp = user;
          var token = generateJWT.GenerateJWTToken(user);
          return Ok(token);
        }
        if (result.RequiresTwoFactor)
        {
          return BadRequest("Locked by two factor authentication, use the web instead and stay tuned for a non beta version");
        }
        if (result.IsLockedOut)
        {
        
          return BadRequest("User is locked out, return in 1 hour time");
        }
        else
        {
          return BadRequest("Invalid login attempt");
        }
      }
      return BadRequest("Invalid login attempt");
    }


   

  }
}
