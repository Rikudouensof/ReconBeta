using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReconBeta.Models
{
  public class Emergency_Numbers
  {
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

   [Display(Name = "Phone Number")]
   [Required]
    public int PhoneNumber { get; set; }
  }
}
