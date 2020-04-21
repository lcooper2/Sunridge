using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Hosting;
using Sunridge.Models.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Sunridge.Utility;
using Microsoft.AspNetCore.Identity;

namespace Sunridge.Pages.Dashboard.OwnerDash.MyPhotos.Photos
{
    [Authorize(Roles = SD.Owner)]
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly UserManager<IdentityUser> _userManager;

        public UpsertModel(UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
        }

        [BindProperty]
        public UserPhotosVM userPhotosObj { get; set; }

        public IActionResult OnGet(int? id)
        {
            userPhotosObj = new UserPhotosVM()
            {
                CategoryList =_unitOfWork.UserPhotoCategory.GetCategoryListForDropDown(),

                UserPhotos = new Models.UserPhotos()
            };


            if (id != null)// edit
            {
                userPhotosObj.UserPhotos = _unitOfWork.UserPhotos.GetFirstOrDefault(u => u.Id == id);
                if (userPhotosObj == null)
                {
                    return NotFound();
                }
            }
            return Page();
        }


        public async Task<IActionResult> OnPost()
        {
            // Find the root path
            string webRootPath = _hostingEnvironment.WebRootPath;
            // grab the file(s)
            var files = HttpContext.Request.Form.Files;
            string images = userPhotosObj.UserPhotos.Image;
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (userPhotosObj.UserPhotos.Id == 0)
            {

                // rename the file user submited
                string fileName = Guid.NewGuid().ToString();

                // upload to path
                var uploads = Path.Combine(webRootPath, @"Images\UsersPhotoGallery");

                // preserve our extentions
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))

                {
                    files[0].CopyTo(fileStream);
                }

                userPhotosObj.UserPhotos.Image = @"Images\UsersPhotoGallery\" + fileName + extension;
                var user = await _userManager.GetUserAsync(User);
                userPhotosObj.UserPhotos.UserId = user.Id;
                _unitOfWork.UserPhotos.Add(userPhotosObj.UserPhotos);
            }
            else // edit 
            {
                var objFromDb = _unitOfWork.UserPhotos.Get(userPhotosObj.UserPhotos.Id);
                // any file submitted with post
                if (files.Count > 0)
                {
                    // rename the file user submited
                    string fileName = Guid.NewGuid().ToString();

                    // upload to path
                    var uploads = Path.Combine(webRootPath, @"Images\UsersPhotoGallery");

                    // preserve our extentions
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

                    userPhotosObj.UserPhotos.Image = @"Images\UsersPhotoGallery\" + fileName + extension;

                }
                else
                {
                    userPhotosObj.UserPhotos.Image = objFromDb.Image;

                }
                
                _unitOfWork.UserPhotos.Update(userPhotosObj.UserPhotos);
            }

            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }

    }
}