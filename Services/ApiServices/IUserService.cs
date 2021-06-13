using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using ReconBeta.ApiClasses.Models;
using ReconBeta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ReconBeta.Services.ApiServices
{
  public interface IUserService
  {
    Task<UserManagerResponse> RegisterUserAsync(ApiRegister model);
  }

  public class UserService : IUserService
  {
    private  UserManager<User> _userManager;


    public UserService(UserManager<User> userManager)
    {

      _userManager = userManager;
    }

    public async Task<UserManagerResponse> RegisterUserAsync(ApiRegister model)
    {
      if (model == null)
      {
        throw new NullReferenceException("Register Model is null");
       
      }
      try
      {
        var userExist = await _userManager.FindByNameAsync(model.Email);
        if (userExist != null)
        {
          return new UserManagerResponse { Status = "Error", Message = "User Already Exist", IsSuccess = false };
        }
      }
      catch
      {

   
      }
     


      if (model.Password != model.ConfirmPassword)
      {
        return new UserManagerResponse
        {
          Message = "Your passwords does not match",
          IsSuccess = false,
          Status = "Not successful"

        };
      }


      User user = new User()
      {
        Email = model.Email,
        SecurityStamp = Guid.NewGuid().ToString(),
        UserName = model.Email

      };

      var result = await _userManager.CreateAsync(user, model.Password);
     



      if (result.Succeeded)
      {


      
        return new UserManagerResponse
        {
          Message = "Your account has been created successfully !",
          Status = "Success",
          IsSuccess = true,


        };
      }

      return new UserManagerResponse
      {
        Message = "User was not created",
        IsSuccess = false,
        Errors = result.Errors.Select(e => e.Description),
        Status = "Not successsful"
      };
    }
  }
}
