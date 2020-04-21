using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Utility;

namespace Sunridge.Pages.Dashboard.AdminDash.Banner
{
    [Authorize(Roles = SD.AdminRole)]
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public UpsertModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        [BindProperty]
        public Models.Banner BannerObj { get; set; }



        public IActionResult OnGet(int? id)
        {
            BannerObj = new Models.Banner();

            if (id != null)// allows edit to be used
            {
                BannerObj = _unitOfWork.Banner.GetFirstOrDefault(s => s.Id == id);
                if (BannerObj == null) // catches the exception if it can not find the banner object
                {
                    return NotFound();
                }
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            // Find the root path
            string webRootPath = _hostingEnvironment.WebRootPath;
            // grab the file(s)
            var files = HttpContext.Request.Form.Files;

            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (BannerObj.Id == 0) // checking to see if it is a new Banner
            {
                // rename the file user submited
                string fileName = Guid.NewGuid().ToString();

                // upload to path
                var uploads = Path.Combine(webRootPath, @"Images\BannerImages");

                // preserve our extentions
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))

                {
                    files[0].CopyTo(fileStream);
                }

                BannerObj.Image = @"Images\BannerImages\" + fileName + extension;
                _unitOfWork.Banner.Add(BannerObj);
            }
            else // if is not then it is an existing object that is being edit and updated
            {
                var objFromDb = _unitOfWork.Banner.Get(BannerObj.Id);

                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();

                    var uploads = Path.Combine(webRootPath, @"Images\BannerImages");

                    var extension = Path.GetExtension(files[0].FileName);

                    var imagePath = Path.Combine(webRootPath, objFromDb.Image.TrimStart('\\'));

                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    BannerObj.Image = @"Images\BannerImages\" + fileName + extension;

                }
                else
                {
                    BannerObj.Image = objFromDb.Image;
                }

                _unitOfWork.Banner.Update(BannerObj);
            }

            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }

    }
}