using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.Pages.Blog
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            Threads = new List<Thread>();
        }

        [BindProperty]
        public IEnumerable<Thread> Threads { get; set; }
        public void OnGet()
        {
            Threads = new List<Thread>();
            ApplicationUser user = _unitOfWork.ApplicationUser.Get("da7e33e7-b1c5-464d-b52c-a0b6e8da5188");
            Thread thread1 = new Thread();
            thread1.ApplicationUserId = user.Id;
            thread1.ApplicationUser = user;
            thread1.WhenPosted = DateTime.Now;
            thread1.Comments = new List<Comment>();

            Thread thread2 = new Thread();
            thread2.ApplicationUserId = user.Id;
            thread2.ApplicationUser = user;
            thread2.WhenPosted = DateTime.Now;
            thread2.Comments = new List<Comment>();

            Thread thread3 = new Thread();
            thread3.ApplicationUserId = user.Id;
            thread3.ApplicationUser = user;
            thread3.WhenPosted = DateTime.Now;
            thread3.Comments = new List<Comment>();

            Comment comment1 = new Comment();
            comment1.ThreadId = thread1.Id;
            comment1.CommentText = "This is a test of the emergency broadcast system";
            comment1.WhenPosted = thread1.WhenPosted.AddSeconds(30);
            comment1.ApplicationUserId = user.Id;

            Comment comment2 = new Comment();
            comment2.ThreadId = thread2.Id;
            comment2.CommentText = "This is a test of the emergency broadcast system";
            comment2.WhenPosted = thread2.WhenPosted.AddSeconds(30);
            comment2.ApplicationUserId = user.Id;

            Comment comment3 = new Comment();
            comment3.ThreadId = thread3.Id;
            comment3.CommentText = "This is a test of the emergency broadcast system";
            comment3.WhenPosted = thread3.WhenPosted.AddSeconds(30);
            comment3.ApplicationUserId = user.Id;

            thread1.Comments.Add(comment1);
            thread2.Comments.Add(comment2);
            thread3.Comments.Add(comment3);

            _unitOfWork.Thread.Add(thread1);
            _unitOfWork.Thread.Add(thread2);
            _unitOfWork.Thread.Add(thread3);

            _unitOfWork.Comment.Add(comment1);
            _unitOfWork.Comment.Add(comment2);
            _unitOfWork.Comment.Add(comment3);

            _unitOfWork.Save();
            Threads = _unitOfWork.Thread.GetAll(t => t.WhenPosted > DateTime.Now.AddDays(-7), t => t.OrderBy(t => t.WhenPosted), "ApplicationUser");
            foreach (var thread in Threads)
            {
                thread.Comments = _unitOfWork.Comment.GetAll
                                (
                                  c => (c.ThreadId == thread.Id) && (c.WhenPosted > DateTime.Now.AddDays(-7)),
                                  orderBy: t => t.OrderBy(t => t.WhenPosted),
                                  "Images"
                                 )
                                 .ToList();
                
            }

        }
    }
}