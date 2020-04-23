﻿using System;
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
            if(files.Count == 0 && BlogComment.BlogCommentText == null) { return Page(); }
            for(int i = 0; i < files.Count; i++)
            {
                string fileName = Guid.NewGuid().ToString();
                var extension = Path.GetExtension(files[i].FileName);
                if(!acceptableExtensions.Contains(extension.ToLower())) { continue; } // Rudimentary security
                using (var filestream = new FileStream(Path.Combine(uploadPath, fileName + extension), FileMode.Create))
                {
                    files[i].CopyTo(filestream);
                }
                BlogImage image = new BlogImage() {
                    BlogCommentId = blogComment.Id,
                    ImgPath = @"\Images\BlogImages\Uploads\" + fileName + extension
                };
                blogComment.Images.Add(image);

                // Compress the image so the page loads faster and takes up less space
                Compress(image.ImgPath);
            }
            BlogThread.BlogComments.Add(blogComment);
            _unitOfWork.BlogThread.Add(BlogThread);
            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }

        public async void Compress(string imagePath)
        {
            var pathToImage = _webHostEnvironment.WebRootPath + imagePath;
            Tinify.Key = "Sqx3cZlxJQjDfB4S5KhNcn8DZKrwKPQV";

            // From and to file paths are the same so that the stored image will be overwritten with the compressed one
            var source = Tinify.FromFile(pathToImage);
            await source.ToFile(pathToImage);
        }
    }
}
