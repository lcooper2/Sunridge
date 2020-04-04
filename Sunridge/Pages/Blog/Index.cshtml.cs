using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
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
        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            BlogThreads = new List<BlogThread>();
        }

        [BindProperty]
        public IEnumerable<BlogThread> BlogThreads { get; set; }
        public void OnGet()
        {
            //BlogThreads = new List<BlogThread>();
            //ApplicationUser user = _unitOfWork.ApplicationUser.Get("15857644-50f9-4a4f-98f1-7b868c59c95a");
            //BlogThread thread1 = new BlogThread();
            //thread1.ApplicationUserId = user.Id;
            //thread1.ApplicationUser = user;
            //thread1.WhenPosted = DateTime.Now;
            //thread1.BlogComments = new List<BlogComment>();

            //BlogThread thread2 = new BlogThread();
            //thread2.ApplicationUserId = user.Id;
            //thread2.ApplicationUser = user;
            //thread2.WhenPosted = DateTime.Now;
            //thread2.BlogComments = new List<BlogComment>();

            //BlogThread thread3 = new BlogThread();
            //thread3.ApplicationUserId = user.Id;
            //thread3.ApplicationUser = user;
            //thread3.WhenPosted = DateTime.Now;
            //thread3.BlogComments = new List<BlogComment>();

            //BlogComment comment1 = new BlogComment();
            //comment1.BlogThreadId = thread1.Id;
            //comment1.BlogCommentText = "This is a test of the emergency broadcast system";
            //comment1.WhenPosted = thread1.WhenPosted.AddSeconds(30);
            //comment1.ApplicationUserId = user.Id;

            //BlogComment comment2 = new BlogComment();
            //comment2.BlogThreadId = thread2.Id;
            //comment2.BlogCommentText = "This is a test of the emergency broadcast system";
            //comment2.WhenPosted = thread2.WhenPosted.AddSeconds(30);
            //comment2.ApplicationUserId = user.Id;

            //BlogComment comment3 = new BlogComment();
            //comment3.BlogThreadId = thread3.Id;
            //comment3.BlogCommentText = "This is a test of the emergency broadcast system";
            //comment3.WhenPosted = thread3.WhenPosted.AddSeconds(30);
            //comment3.ApplicationUserId = user.Id;

            //thread1.BlogComments.Add(comment1);
            //thread2.BlogComments.Add(comment2);
            //thread3.BlogComments.Add(comment3);

            //_unitOfWork.BlogThread.Add(thread1);
            //_unitOfWork.BlogThread.Add(thread2);
            //_unitOfWork.BlogThread.Add(thread3);

            //_unitOfWork.Save();
            BlogThreads = _unitOfWork.BlogThread.GetAll(t => t.WhenPosted > DateTime.Now.AddDays(-7), t => t.OrderBy(t => t.WhenPosted), "ApplicationUser");
            foreach (var thread in BlogThreads)
            {
                thread.BlogComments = _unitOfWork.BlogComment.GetAll
                                (
                                  c => (c.BlogThreadId == thread.Id) && (c.WhenPosted > DateTime.Now.AddDays(-7)),
                                  orderBy: t => t.OrderByDescending(t => t.WhenPosted),
                                  "Images"
                                 )
                                 .ToList();

            }

        }
    }
}