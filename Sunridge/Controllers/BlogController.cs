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
        public int numPosts = 2;

        public BlogController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public int IncNumPosts()
        {
            numPosts += numPosts;
            return numPosts;
        }

        [HttpPost("{id}")]
        public int OnPostToggleLike(int id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var userHasAlreadyLiked = _unitOfWork.BlogLike.GetFirstOrDefault
                (bl => bl.BlogCommentId == id && bl.ApplicationUserId == claim.Value);

            // Post has already been liked. Unlike it.
            if (userHasAlreadyLiked != null) 
            {
                _unitOfWork.BlogLike.Remove(userHasAlreadyLiked);
                _unitOfWork.Save();
                return GetNumLikes(id);
            }

            BlogLike like = new BlogLike()
            {
                ApplicationUserId = claim.Value,
                BlogCommentId = id
            };

            _unitOfWork.BlogLike.Add(like);
            _unitOfWork.Save();
            return GetNumLikes(id);
        }

        [HttpDelete("{threadId}")]
        [ActionName("Delete")]
        public bool OnPostDelete(int threadId)
        {
            try
            {
                _unitOfWork.BlogThread.DeleteThread(threadId);
                _unitOfWork.Save();
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }

        public int GetNumLikes(int id)
        {
            return _unitOfWork.BlogLike.GetAll(l => l.BlogCommentId == id).Count();
        }
    }
}