using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ReconBeta.ApiClasses.Models;
using ReconBeta.Models;
using ReconBeta.Services.ApiServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReconBeta.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ApiRegisterController : ControllerBase
  {
    
    private IUserService _userService;

    public ApiRegisterController(IUserService userService)
    {
     
      _userService = userService;
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
  }
}
