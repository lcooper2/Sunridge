using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sunridge.DataAccess.Data;
using Sunridge.Models;

namespace Sunridge.Pages.Dashboard.AdminDash.News
{
    public class DetailsModel : PageModel
    {
        private readonly Sunridge.DataAccess.Data.ApplicationDbContext _context;

        public DetailsModel(Sunridge.DataAccess.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public NewsItem NewsItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            NewsItem = await _context.NewsItem.FirstOrDefaultAsync(m => m.NewsItemId == id);

            if (NewsItem == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
