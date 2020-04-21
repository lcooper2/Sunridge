using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.Utility;

namespace Sunridge.Pages.Dashboard.OwnerDash.MyPhotos.Photos
{
    [Authorize(Roles = SD.Owner)]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {

        }
    }
}