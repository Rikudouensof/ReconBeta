using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReconBeta.Models
{
  public class UserImages
  {
    [Key]
    public string Id { get; set; }

    public IFormFile UploadFile { get; set; }

  }
}
