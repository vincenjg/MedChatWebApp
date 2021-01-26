using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApiCore.Models
{
    public class WebAPICoreContext : DbContext
    {
        public WebAPICoreContext(DbContextOptions<WebAPICoreContext> options)
            : base(options)
        {

        }
        public DbSet<Patient> Patients { get; set; }

    }
}
