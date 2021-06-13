using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using ReconBeta.Data;
using ReconBeta.Models;
using ReconBeta.Services.ApiServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReconBeta
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      //EF and Context
      services.AddDbContext<ApplicationDbContext>(options =>
          options.UseSqlServer(
              Configuration.GetConnectionString("DefaultConnection")));
      services.AddDatabaseDeveloperPageExceptionFilter();

      //Identity
      services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
         .AddRoles<IdentityRole>()
          .AddEntityFrameworkStores<ApplicationDbContext>()
         .AddDefaultTokenProviders();


      ////Authentication
      //services.AddAuthentication(option =>
      //{
      //  option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      //  option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      //  option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
      //})
      //  .AddJwtBearer(options =>
      //  {
      //    options.SaveToken = true;
      //    options.RequireHttpsMetadata = false;
      //    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
      //    {
      //      ValidateIssuer = true,
      //      ValidateAudience = true,
      //      ValidAudience = "http://reconbeta-test.us-east-1.elasticbeanstalk.com/",
      //      ValidIssuer = "http://reconbeta-test.us-east-1.elasticbeanstalk.com/",
      //      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("This is the encryption key")),
      //      RequireExpirationTime = true,
      //    };
      //  });

      services.AddScoped<IUserService, UserService>();
      services.AddControllersWithViews().AddRazorRuntimeCompilation();
      services.AddRazorPages();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseMigrationsEndPoint();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }
      
      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}");
        endpoints.MapRazorPages();
      });
    }
  }
}
