using System;
using System.Collections.Generic;
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
        public int numDays = -7; // Default number of days back to load posts

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

            BlogThreads = _unitOfWork.BlogThread.LoadAll(numDays);
            foreach(var thread in BlogThreads)
            {
                for(int i = 0; i < thread.BlogComments.Count; i++)
                {
                    thread.BlogComments[i].Images = _unitOfWork.BlogImage.GetAll(j => j.BlogCommentId == thread.BlogComments[i].Id).ToList();
                } 
            }
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
            return (result == null);
        }

        public string GetCurrentUserId()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if(claim.Value == null) { return ""; }
            return claim.Value;
        }
        public int GetNumLikes(int commentId)
        {
            return _unitOfWork.BlogLike.GetAll(l => l.BlogCommentId == commentId).Count();
        }
    }
}