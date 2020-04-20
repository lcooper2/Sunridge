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

namespace Sunridge.Pages.Blog
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public string CurrentAppUserId;
        public int totalPosts;
        public int numPosts = 2; // Default number of posts to show. Must match number in blog.js

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

            BlogReply reply = new BlogReply()
            {
                BlogCommentId = id,
                ReplyText = comment,
                WhenPosted = DateTime.Now,
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
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null) { return RedirectToPage("Blog"); }
            var selector = "textarea(" + threadId.ToString() + ")";
            var comment = Request.Form[selector];

            BlogComment blogComment = new BlogComment()
            {
                ApplicationUserId = claim.Value,
                BlogThreadId = threadId,
                BlogCommentText = comment,
                WhenPosted = DateTime.Now,
                Images = new List<BlogImage>()
            };

            string webRootPath = _webHostEnvironment.WebRootPath;
            var uploadPath = Path.Combine(webRootPath, @"Images\BlogImages\Uploads");
            List<string> acceptableExtensions = new List<string>() { ".jpg", ".jpeg", ".jfe", ".jfif", ".bmp", ".png", ".gif" };

            var files = HttpContext.Request.Form.Files;
            for (int i = 0; i < files.Count; i++)
            {
                string fileName = Guid.NewGuid().ToString();
                var extension = Path.GetExtension(files[i].FileName);
                if (!acceptableExtensions.Contains(extension.ToLower())) { continue; } // Rudimentary security
                using (var filestream = new FileStream(Path.Combine(uploadPath, fileName + extension), FileMode.Create))
                {
                    files[i].CopyTo(filestream);
                }
                BlogImage image = new BlogImage()
                {
                    BlogCommentId = blogComment.Id,
                    ImgPath = @"\Images\BlogImages\Uploads\" + fileName + extension
                };
                blogComment.Images.Add(image);
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

        public int GetImageEXIFOrientation(string imgPath)
        {
            var webRootPath = _webHostEnvironment.WebRootPath;
            int exifOrientationID = 0x112; //274
            int val = 0;
            try
            {
                Image img = new Bitmap(webRootPath + imgPath);
                img.Dispose();
            }
            catch
            {
                return -1; // Failed to create image with given path
            }
            using (Image image = new Bitmap(webRootPath + imgPath))
            {
                try
                {
                    var prop = image.GetPropertyItem(exifOrientationID);
                    val = BitConverter.ToUInt16(prop.Value, 0);
                    if (val == 0)
                    {
                        if (image.Width > image.Height) { val = 1; } // Lanscape
                        else { val = 6; } // Portrait
                    }
                    return val;
                }
                catch // No exif data
                {
                    if (image.Width > image.Height) { val = 1; } // Lanscape
                    else { val = 6; } // Portrait
                    return val;
                }
            }
        }

        public List<string> GetImageLayout(List<BlogImage> images)
        {
            List<int> layouts = new List<int>();
            for (int i = 0; i < images.Count(); i++)
            {
                // If image has EXIF orientation info, add it to our list.
                var orient = GetImageEXIFOrientation(images[i].ImgPath); // After changes will always return a value
                layouts.Add(orient);
            }

            List<string> imgTags = new List<string>();

            for (int i = 0; i < layouts.Count(); i++)
            {
                switch (layouts[i])
                {
                    case 1: // Landscape
                        var tag1 = "<img src=" + "'" + images[i].ImgPath + "'" + " onclick='Modal(" + images[i].Id + ")'" + "class='myImg landscape embed-responsive-4by3 m1' id='img(" + images[i].Id + ")'" +  " />";
                        imgTags.Add(tag1);
                        break;

                    case 3: // Lanscape other way
                        var tag3 = "<img src=" + "'" + images[i].ImgPath + "'" + " onclick='Modal(" + images[i].Id + ")'" + "class='myImg landscape embed-responsive-4by3 m1' id='img(" + images[i].Id + ")'" + " />";
                        imgTags.Add(tag3);
                        break;

                    case 6: // Portrait
                        var tag6 = "<img src=" + "'" + images[i].ImgPath + "'" + " onclick='Modal(" + images[i].Id + ")'" + "class='myImg portrait embed-responsive-4by3 m1' id='img(" + images[i].Id + ")'" + " />";
                        imgTags.Add(tag6);
                        break;
                    case 8:
                        var tag8 = "<img src=" + "'" + images[i].ImgPath + "'" + " onclick='Modal(" + images[i].Id + ")'" + "class='myImg portrait embed-responsive-4by3 m1' id='img(" + images[i].Id + ")'" + " />";
                        imgTags.Add(tag8);
                        break;
                    case -1:
                        imgTags.Add("");
                        break;
                }
            }
            imgTags.Sort();
            return imgTags;
        }
    }
}