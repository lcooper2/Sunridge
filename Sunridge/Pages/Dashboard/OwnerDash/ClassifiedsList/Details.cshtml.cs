using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sunridge.DataAccess.Data;
using Sunridge.Models;

namespace Sunridge
{
    public class DetailsModel : PageModel
    {
        private readonly Sunridge.DataAccess.Data.ApplicationDbContext _context;

        public DetailsModel(Sunridge.DataAccess.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public ClassifiedListing ClassifiedListing { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ClassifiedListing = await _context.ClassifiedListing
                .Include(c => c.ApplicationUser)
                .Include(c => c.ClassifiedCategory).FirstOrDefaultAsync(m => m.Id == id);

            if (ClassifiedListing == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
