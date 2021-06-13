using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReconBeta.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReconBeta.Data
{
  public class ApplicationDbContext : IdentityDbContext<User>
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<ContactUs> ContactUs { get; set; }

    public DbSet<GalaryImages> GalaryImages { get; set; }

    public DbSet<Stores> Stores { get; set; }
  }
}
