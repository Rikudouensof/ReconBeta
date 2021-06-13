using ReconBeta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReconBeta.ViewModels
{
  public class GeneralViewModel
  {
    public ContactUs ContactUs { get; set; }

    public IEnumerable<GalaryImages> GalaryImages { get; set; }

    public Stores Store { get; set; }


  }
}
