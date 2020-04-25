using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
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
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public string CurrentAppUserId;
        public int totalPosts;
        public int numPosts = 10; // Default number of posts to show. Must match number in blog.js

        public IndexModel(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            BlogThreads = new List<BlogThread>();
        }

        [BindProperty]
        public IEnumerable<BlogThread> BlogThreads { get; set; }
        public void OnGet()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            CurrentAppUserId = claim.Value;
            BlogThreads = _unitOfWork.BlogThread.LoadAll();
            totalPosts = BlogThreads.Count();
        }

        public IActionResult OnPostReply(int id)
        {
            var selector = "reply(" + id.ToString() + ")";
            var comment = Request.Form[selector];
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var user = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value);

            BlogReply reply = new BlogReply()
            {
                BlogCommentId = id,
                ReplyText = comment,
                WhenPosted = DateTime.Now,
                ApplicationUserId = claim.Value,
                ApplicationUser = user
            };
            _unitOfWork.BlogReply.Add(reply);
            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }

        public bool UserHasLikedPost(int commentId)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var result = _unitOfWork.BlogLike.GetFirstOrDefault(c => c.ApplicationUserId == claim.Value && c.BlogCommentId == commentId);
            return (result != null);
        }

        public IActionResult OnPostComment(int threadId)
        {
            var selector = "textarea(" + threadId.ToString() + ")";
            var comment = Request.Form[selector];

            BlogComment blogComment = new BlogComment()
            {
                ApplicationUserId = GetCurrentUserId(),
                BlogThreadId = threadId,
                BlogCommentText = comment,
                WhenPosted = DateTime.Now,
                Images = new List<BlogImage>()
            };

            string webRootPath = _webHostEnvironment.WebRootPath;
            var uploadPath = Path.Combine(webRootPath, @"Images\BlogImages\Uploads");
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
                        BlogImage image = new BlogImage()
                        {
                            BlogCommentId = blogComment.Id,
                            ImgPath = @"\Images\BlogImages\Uploads\" + fileName + extension
                        };
                        blogComment.Images.Add(image);
                        imgList.Add(image.ImgPath);
                    }
                }
            }
            // For some reason, there are fewer issues with compressing images
            // in this loop than when I do it in the one above.
            foreach (var img in imgList)
            {
                Compress(img);
            }
            _unitOfWork.BlogComment.Add(blogComment);
            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }

        public string GetCurrentUserId()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim.Value == null) { return ""; }
            return claim.Value;
        }

        public string AssignClassTag(string imgPath)
        {
            var webRootPath = _webHostEnvironment.WebRootPath;
            var orientation = "";
            try
            {
                using (Image image = new Bitmap(webRootPath + imgPath))
                {
                    if (image.Width > image.Height) { orientation = "landscape"; } // Lanscape
                    else { orientation = "portrait"; } // Portrait
                }
                return orientation;
            }
            catch
            {
                return ""; // Failed to create image with given path
            }
            
        }

        // This method tries to determine the layout for each image in 
        // a list of images and generates an html tag with the appropriate 
        // class marking it as either landscape or portrait.
        public List<string> GetImageClassTags(List<BlogImage> images)
        {
            images = images.OrderBy(i => i.Id).ToList();
            List<string> tags = new List<string>();
            for (int i = 0; i < images.Count(); i++)
            {
                var orient = AssignClassTag(images[i].ImgPath);
                tags.Add(orient);
            }
            return tags;
        }

        public async void Compress(string imagePath)
        {
            var pathToImage = _webHostEnvironment.WebRootPath + imagePath;
            Tinify.Key = "Sqx3cZlxJQjDfB4S5KhNcn8DZKrwKPQV";

            try
            {
                var source = Tinify.FromFile(pathToImage);
                var resized = source.Resize(new
                {
                    method = "scale",
                    height = 750
                });
                await resized.ToFile(pathToImage);
            }
            catch
            {
                // Intentionally do nothing. Image just doesn't get compressed.
            }
        }
    }
}