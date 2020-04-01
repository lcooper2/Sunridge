using System;
using System.Collections.Generic;
using System.Linq;
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
        // Method is called OnPostLikeThread, but it really just likes the
        // chronologically first comment in a thread, which is the users.
        public void OnPostLikeThread(string applicationUserId, int commentId)
        {
            BlogLike like = new BlogLike()
            {
                ApplicationUserId = applicationUserId,
                CommentId = commentId
            };
            _unitOfWork.Like.Add(like);
            _unitOfWork.Save();
        }
    }
}