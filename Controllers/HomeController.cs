using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReconBeta.Data;
using ReconBeta.Models;
using ReconBeta.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ReconBeta.Controllers
{
  public class HomeController : Controller
  {
        private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ApplicationDbContext db;
    private readonly ILogger<HomeController> _logger;
    
    public HomeController(ILogger<HomeController> logger, ApplicationDbContext _db, UserManager<User> userManager,
            SignInManager<User> signInManager)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _logger = logger;
      db = _db;
    }

    public IActionResult Index()
    {
      if (User.IsInRole("Admin"))
      {
        return RedirectToAction("Index","ContactUs");
      }
      GeneralViewModel generalViewModel = new GeneralViewModel()
      {
        GalaryImages = db.GalaryImages,
        Store = db.Stores.OrderByDescending(m => m.Id).FirstOrDefault()
      };
      return View(generalViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Index(GeneralViewModel generalViewModel)
    {
      generalViewModel.GalaryImages = db.GalaryImages;
      generalViewModel.Store = db.Stores.OrderByDescending(m => m.Id).FirstOrDefault();
      if (ModelState.IsValid)
      {
        db.ContactUs.Add(generalViewModel.ContactUs);
        await db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      return View(generalViewModel);
    }

    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
