using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReconBeta.Data;
using ReconBeta.Models;

namespace ReconBeta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ApiUserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

    public static IWebHostEnvironment _environment;

        public ApiUserController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
      _environment = environment;
        }

        // GET: api/ApiUserImages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUserImages()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/ApiUserImages/5
        [HttpGet]
        public async Task<ActionResult<UserImages>> GetUserImages(string id)
        {
            var userImages = await _context.UserImages.FindAsync(id);

            if (userImages == null)
            {
                return NotFound();
            }

            return userImages;
        }



        // PUT: api/ApiUserImages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutUserImages(string id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                
                    return NotFound();
               
                
            }

            return NoContent();
        }

        // POST: api/ApiUserImages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserImages>> PostUserImages(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserImages", new { id = user.Id }, user);
        }


    [HttpPost]
    public async Task<ActionResult<string>> UploadUserImage(UserImages userImages)
    {
      var user = _context.Users.Where(m => m.Id == userImages.Id).FirstOrDefault();
      var guild = Guid.NewGuid().ToString();
      if (userImages.UploadFile.Length > 0)
      {
        if (!Directory.Exists(_environment.WebRootPath + "\\ProfilePicture\\"))
        {
          Directory.CreateDirectory(_environment.WebRootPath + "\\ProfilePicture\\");
        }
        using(FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\ProfilePicture\\" + guild + userImages.UploadFile.FileName))
        {
          await userImages.UploadFile.CopyToAsync(fileStream);
          await fileStream.FlushAsync();
          string answer = "\\ProfilePicture\\" + guild + userImages.UploadFile.FileName;
          user.ImageName = answer;
          _context.Users.Update(user);
          _context.SaveChanges();
          return answer;
        }
      }
      else
      {
        return "file upload failed";
      }

      if (userImages == null)
      {
        return NotFound();
      }

      return "Compound Error: data incomplete or wrong";
    }
    // DELETE: api/ApiUserImages/5
    [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserImages(string id)
        {
            var userImages = await _context.Users.FindAsync(id);
            if (userImages == null)
            {
                return NotFound();
            }

            _context.Users.Remove(userImages);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserImagesExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
