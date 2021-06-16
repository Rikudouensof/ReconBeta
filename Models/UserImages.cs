using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReconBeta.Models
{
  public class UserImages
  {
    [Key]
    public string Id { get; set; }

    [NotMapped]
    public IFormFile UploadFile { get; set; }

  }
}
