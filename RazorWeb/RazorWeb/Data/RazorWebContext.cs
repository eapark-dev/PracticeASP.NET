using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RazorWeb.Models;

namespace RazorWeb.Data
{
    public class RazorWebContext : DbContext
    {
        public RazorWebContext (DbContextOptions<RazorWebContext> options)
            : base(options)
        {
        }

        public DbSet<RazorWeb.Models.Movie> Movie { get; set; }
    }
}
