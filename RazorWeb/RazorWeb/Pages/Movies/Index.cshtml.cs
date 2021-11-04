using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorWeb.Data;
using RazorWeb.Models;

namespace RazorWeb.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly RazorWeb.Data.RazorWebContext _context;

        public IndexModel(RazorWeb.Data.RazorWebContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; }

        public async Task OnGetAsync()
        {
            Movie = await _context.Movie.ToListAsync();
        }
    }
}
