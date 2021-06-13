using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReconBeta.Constants;
using ReconBeta.Data;
using ReconBeta.Models;

namespace ReconBeta.Controllers
{
  [Authorize(Roles = "Admin")]
  public class GalaryImagesController : Controller
  {
    private readonly ApplicationDbContext _context;
    private IHostingEnvironment _env;
    public GalaryImagesController(IHostingEnvironment env, ApplicationDbContext context)
    {
      _context = context;
      _env = env;
    }

    // GET: GalaryImages
    public async Task<IActionResult> Index()
    {
      return View(await _context.GalaryImages.ToListAsync());
    }

    // GET: GalaryImages/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var galaryImages = await _context.GalaryImages
          .FirstOrDefaultAsync(m => m.Id == id);
      if (galaryImages == null)
      {
        return NotFound();
      }

      return View(galaryImages);
    }

    // GET: GalaryImages/Create
    public IActionResult Create()
    {
      return View();
    }

    // POST: GalaryImages/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(GalaryImages galaryImages)
    {
      try
      {
        var iscorrectformat = false;
        string uniqueName = null;
        string filePath = null;
        FileInfo fi = new FileInfo(galaryImages.UploadedFile.FileName);

        var actualextension = fi.Extension;
        var imageextensions = FileFormat.GetSupportedImageTypeExtensionsList();
        foreach (var imageExtension in imageextensions)
        {
          if (imageExtension == actualextension)
          {
            iscorrectformat = true;
          }
        }
        if (iscorrectformat == false)
        {
          return View(galaryImages);
        }

        if (galaryImages.UploadedFile != null)
        {
          string uploadsFolder = Path.Combine(_env.WebRootPath, "Images");
          uniqueName = Guid.NewGuid().ToString() + "_" + galaryImages.UploadedFile.FileName;
          filePath = Path.Combine(uploadsFolder, uniqueName);
          galaryImages.UploadedFile.CopyTo(new FileStream(filePath, FileMode.Create));
          galaryImages.ImageName = uniqueName;
        }
      }
      catch
      {
        return View(galaryImages);
      }
      if (ModelState.IsValid)
      {
      
        _context.Add(galaryImages);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      return View(galaryImages);
    }

    // GET: GalaryImages/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var galaryImages = await _context.GalaryImages.FindAsync(id);
      if (galaryImages == null)
      {
        return NotFound();
      }
      return View(galaryImages);
    }

    // POST: GalaryImages/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ImageName")] GalaryImages galaryImages)
    {
      if (id != galaryImages.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(galaryImages);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!GalaryImagesExists(galaryImages.Id))
          {
            return NotFound();
          }
          else
          {
            throw;
          }
        }
        return RedirectToAction(nameof(Index));
      }
      return View(galaryImages);
    }

    // GET: GalaryImages/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var galaryImages = await _context.GalaryImages
          .FirstOrDefaultAsync(m => m.Id == id);
      if (galaryImages == null)
      {
        return NotFound();
      }

      return View(galaryImages);
    }

    // POST: GalaryImages/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var galaryImages = await _context.GalaryImages.FindAsync(id);
      _context.GalaryImages.Remove(galaryImages);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool GalaryImagesExists(int id)
    {
      return _context.GalaryImages.Any(e => e.Id == id);
    }
  }
}
