using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReconBeta.Models
{
  public class ContactUs
  {
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    public int Phone { get; set; }

    [Required]
    public string Message { get; set; }
  }
}
