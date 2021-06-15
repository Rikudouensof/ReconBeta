using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ReconBeta.Data;
using ReconBeta.Models;
using ReconBeta.Services.ApiServices;
using System.Text;

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


      var key = Encoding.UTF8.GetBytes(Configuration["Jwt:Secret"]);

      services.AddAuthentication(x =>
      {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
        .AddJwtBearer(x =>
        {
          x.RequireHttpsMetadata = false;
          x.SaveToken = true;
          x.TokenValidationParameters = new TokenValidationParameters
          {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidIssuers = new string[] { Configuration["Jwt:Issuer"] },
            ValidAlgorithms = new string[] { Configuration["Jwt:Issuer"] },
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true
          };
        });










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
      //services.AddSwaggerGen(c =>
      //{
      //  c.SwaggerDoc("v1", new OpenApiInfo { Title = "ReconBeta", Version = "v1" });
      //});
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

      ////app.UseSwagger();
      ////app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ReconBeta v1"));
      ////app.UseDeveloperExceptionPage();


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
