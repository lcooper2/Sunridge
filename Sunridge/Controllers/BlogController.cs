using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public BlogController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public void OnPostLikeThread(int id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if(claim == null){ return; }

            var userHasAlreadyLiked = _unitOfWork.BlogLike.GetFirstOrDefault
                (bl => bl.BlogCommentId == id && bl.ApplicationUserId == claim.Value) != null ? true : false;

            // Prevent a post from being liked multiple times
            if(userHasAlreadyLiked) { return; }

            BlogLike like = new BlogLike()
            {
                ApplicationUserId = claim.Value,
                BlogCommentId = id
            };

            _unitOfWork.BlogLike.Add(like);
            _unitOfWork.Save();
        }

        [HttpPost]
        public void OnPostComment(string comment, int threadId)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null) { return; }

            BlogComment blogComment = new BlogComment()
            {
                ApplicationUserId = claim.Value,
                BlogThreadId = threadId,
                BlogCommentText = comment,
                WhenPosted = DateTime.Now
            };

            _unitOfWork.BlogComment.Add(blogComment);
            _unitOfWork.Save();
        }
    }
}