using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.Pages.Dashboard.AdminDash.News
{
    [BindProperties]
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly UserManager<IdentityUser> _userManager;

        public UpsertModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment
            , UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
        }

        public NewsItem Item { get; set; }
        public List<SelectListItem> YearList { get; set; }
        public IActionResult OnGet(int? id)
        {
            Item = new NewsItem
            {
                Content = "",
                Header = "",
                Year = 2020
            };
            YearList = new List<SelectListItem>();
            for (int i = 2017; i <= DateTime.Now.Year; i++)
            {
                YearList.Add(new SelectListItem()
                {
                    Text = i.ToString(),
                    Value = i.ToString()
                });
            }

            if (id != null)
            {
                Item = _unitOfWork.NewsItem.GetFirstOrDefault(s => s.NewsItemId == id);
                if (Item == null)
                {
                    return NotFound();
                }
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Item.NewsItemId == 0)
            {
                if (files.Count > 0)
                {
                    //string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"docs\News\");
                    //var extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(uploads, files[0].FileName), FileMode.Create))

                    {
                        files[0].CopyTo(fileStream);
                    }
                    Item.FileName = files[0].FileName;
                    Item.FilePath = @"docs\News\" + Item.FileName;
                }
                _unitOfWork.NewsItem.Add(Item);
            }
            else
            {
                if (files.Count > 0)
                {
                    //string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"docs\News\");
                    //var extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(uploads, files[0].FileName), FileMode.Create))

                    {
                        files[0].CopyTo(fileStream);
                    }
                    Item.FileName = files[0].FileName;
                    Item.FilePath = @"docs\News\" + Item.FileName;
                }
                _unitOfWork.NewsItem.Update(Item);
            }

            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }

    }
}