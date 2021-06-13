using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReconBeta.ApiClasses.Models
{
  public class UserManagerResponse
  {
    public string Status { get; set; }
    public string Message { get; set; }

    public bool IsSuccess { get; set; }

    public IEnumerable<string> Errors { get; set; }
  }
}
