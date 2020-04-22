using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Utility;

namespace Sunridge.Pages.Dashboard.AdminDash.LostAndFound
{
    [Authorize(Roles = SD.AdminRole)]
    public class IndexModel : PageModel
    {

        public void OnGet()
        {

        }
    }
}