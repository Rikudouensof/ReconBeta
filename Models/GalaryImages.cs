using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReconBeta.Models
{
  public class GalaryImages
  {
    [Key]
    public int Id { get; set; }

    public string Title { get; set; }

    public string ImageName { get; set; }


    [NotMapped, Display(Name = "Upload File")]
    public IFormFile UploadedFile { get; set; }
  }
}
