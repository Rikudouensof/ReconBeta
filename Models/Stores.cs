using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReconBeta.Models
{
  public class Stores
  {
    [Key]
    public int Id { get; set; }

    [Url]
    public string GooglePlayStore { get; set; }

    [Url]
    public string MicrosoftStore { get; set; }

    [Url]
    public string AppStore { get; set; }


  }
}
