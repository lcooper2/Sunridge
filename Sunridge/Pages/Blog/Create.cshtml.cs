using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using TinifyAPI;

namespace Sunridge.Pages.Blog
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CreateModel(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public BlogComment BlogComment { get; set; }

        public IActionResult OnGet()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            BlogComment = new BlogComment() { ApplicationUserId = claim.Value };
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var whenPosted = DateTime.Now;

            BlogThread BlogThread = new BlogThread()
            {
                ApplicationUserId = claim.Value,
                WhenPosted = whenPosted,
                BlogComments = new List<BlogComment>()
            };
            BlogComment blogComment = new BlogComment()
            {
                ApplicationUserId = claim.Value,
                BlogThreadId = BlogThread.Id,
                WhenPosted = whenPosted,
                BlogCommentText = BlogComment.BlogCommentText,
                Images = new List<BlogImage>()
            };

            string webRootPath = _webHostEnvironment.WebRootPath;
            var uploadPath = Path.Combine(webRootPath, @"Images\BlogImages\Uploads");
            List<string> acceptableExtensions = new List<string>() { ".jpg", ".jpeg", ".jfe", ".jfif", ".bmp", ".png", ".gif" };

            var files = HttpContext.Request.Form.Files;
            if (files.Count == 0 && BlogComment.BlogCommentText == null) { return Page(); }
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
                        BlogImage image = new BlogImage()
                        {
                            BlogCommentId = blogComment.Id,
                            ImgPath = @"\Images\BlogImages\Uploads\" + fileName + extension
                        };
                        blogComment.Images.Add(image);

                        imgList.Add(image.ImgPath);
                        BlogThread.BlogComments.Add(blogComment);
                        _unitOfWork.BlogThread.Add(BlogThread);
                    }
                }
            }
            // For some reason, there are fewer issues with compressing images
            // in this loop than when I do it in the one above.
            foreach(var img in imgList)
            {
                Compress(img);
            }
            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }

        // This method makes use of the TinyPNG API to compress and resize
        // user uploaded images to save bandwidth and increase page load speeds.
        // The original user upload is overwritten with the compressed file.
        // This seems to destroy EXIF orientation data
        public async void Compress(string imagePath)
        {
            var pathToImage = _webHostEnvironment.WebRootPath + imagePath;
            Tinify.Key = "Sqx3cZlxJQjDfB4S5KhNcn8DZKrwKPQV";

            try
            {
                // From and to file paths are the same so that the stored image 
                // will be overwritten with the compressed one.
                // Free tier gets 500 compressions per month
                var source = Tinify.FromFile(pathToImage);
                var resized = source.Resize(new
                {
                    method = "scale",
                    height = 700
                });
                await resized.ToFile(pathToImage);
            }
            catch
            {
                Console.WriteLine("AAAHHHH");
                // Intentionally do nothing. Image just doesn't get compressed.
            }
        }
    }
}
