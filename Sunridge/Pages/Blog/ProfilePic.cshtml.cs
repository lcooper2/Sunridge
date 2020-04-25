using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using TinifyAPI;

namespace Sunridge.Pages.Blog
{
    [Authorize]
    public class ProfilePicModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProfilePicModel(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string webRootPath = _webHostEnvironment.WebRootPath;
            var uploadPath = Path.Combine(webRootPath, @"Images\BlogImages\ProfilePictures");
            List<string> acceptableExtensions = new List<string>() { ".jpg", ".jpeg", ".jfe", ".jfif", ".bmp", ".png", ".gif" };

            var files = HttpContext.Request.Form.Files;
            List<string> imgList = new List<string>();
            for (int i = 0; i < files.Count; i++)
            {
                string fileName = Guid.NewGuid().ToString();
                var extension = Path.GetExtension(files[i].FileName);
                if (!acceptableExtensions.Contains(extension.ToLower())) { continue; } // Rudimentary security
                using (var filestream = new FileStream(Path.Combine(uploadPath, fileName + extension), FileMode.Create))
                {
                    if (files[i].Length < 5242880 && files[i].Length > 250000)
                    {
                        files[i].CopyTo(filestream);
                        var claimsIdentity = (ClaimsIdentity)User.Identity;
                        var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                        var currentUser = _unitOfWork.ApplicationUser.Get(claim.Value);

                        // Delete the old profile picture off the server
                        var imgPath = Path.Combine(webRootPath, currentUser.ProfilePicture.Trim('\\'));
                        if (System.IO.File.Exists(imgPath))
                        {
                            System.IO.File.Delete(imgPath);
                        }
                        
                        currentUser.ProfilePicture = @"\Images\BlogImages\ProfilePictures\" + fileName + extension;
                        imgList.Add(currentUser.ProfilePicture);
                        _unitOfWork.ApplicationUser.Update(currentUser);
                        _unitOfWork.Save();
                    }
                    else
                    {
                        return Page();
                    }
                }
            }
            foreach(var img in imgList) { Compress(img); }
            return RedirectToPage("./Index");
        }

        public async void Compress(string imagePath)
        {
            var pathToImage = _webHostEnvironment.WebRootPath + imagePath;
            Tinify.Key = "Sqx3cZlxJQjDfB4S5KhNcn8DZKrwKPQV";

            try
            {
                // From and to file paths are the same so that the stored image will be overwritten with the compressed one
                var source = Tinify.FromFile(pathToImage);
                var resized = source.Resize(new
                {
                    method = "cover", // Finds the most important part of the image
                    height = 100,
                    width = 100
                });
                await resized.ToFile(pathToImage);
            }
            catch
            {
                Console.WriteLine("AAGGHH");
                // Intentionally do nothing. Image just doesn't get compressed.
            }
        }
    }
}