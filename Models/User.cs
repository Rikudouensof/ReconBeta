using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReconBeta.Models
{
  public class User : IdentityUser
  {
    public DateTime DateofRegistration { get; set; }

    public string Country { get; set; }

    public string State { get; set; }

    public string City { get; set; }

    public string Address { get; set; }

    public string ZipCode { get; set; }

    public string ImageName { get; set; }

    [NotMapped]
    public IFormFile UploadImage { get; set; }
  }
}
