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

        [HttpPost("{id}")]
        public bool OnPostToggleLike(int id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var userHasAlreadyLiked = _unitOfWork.BlogLike.GetFirstOrDefault
                (bl => bl.BlogCommentId == id && bl.ApplicationUserId == claim.Value);

            // Prevent a post from being liked multiple times
            if (userHasAlreadyLiked != null) 
            {
                _unitOfWork.BlogLike.Remove(userHasAlreadyLiked);
                _unitOfWork.Save();
                return false;
            }

            BlogLike like = new BlogLike()
            {
                ApplicationUserId = claim.Value,
                BlogCommentId = id
            };

            _unitOfWork.BlogLike.Add(like);
            _unitOfWork.Save();
            return true; // Post was liked
        }

        //[HttpPost]
        //public IActionResult OnPostComment(string comment, int threadId)
        //{
            
        //}
    }
}