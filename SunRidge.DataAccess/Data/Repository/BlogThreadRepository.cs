using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class BlogThreadRepository : Repository<BlogThread>, IBlogThreadRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BlogThreadRepository(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment) : base(db)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        // Loads all related data for every thread
        public List<BlogThread> LoadAll()
        {
            List<BlogThread> allThreads = _db.BlogThread
                        .Include(t => t.ApplicationUser)
                        .Include(t => t.BlogComments)
                            .ThenInclude(c => c.ApplicationUser)
                        .Include(t => t.BlogComments)
                            .ThenInclude(c => c.Images)
                        .Include(t => t.BlogComments)
                            .ThenInclude(c => c.Likes)
                        .Include(t => t.BlogComments)
                            .ThenInclude(c => c.Replies)
                                .ThenInclude(r => r.ApplicationUser)
                                .ToList();
            return allThreads;
        }
        
        // This shouldn't be necessary... something is wrong with cascades
        public void DeleteThread(int threadId)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;

            var threads = _db.BlogThread.Where(t => t.Id == threadId)
                        .Include(t => t.ApplicationUser)
                        .Include(t => t.BlogComments)
                            .ThenInclude(c => c.Images)
                        .Include(t => t.BlogComments)
                            .ThenInclude(c => c.Likes)
                        .Include(t => t.BlogComments)
                            .ThenInclude(c => c.Replies)
                                .ToList();

            // Delete image files from the server
            foreach (var thread in threads)
            {
                foreach (var comment in thread.BlogComments)
                {
                    foreach (var img in comment.Images)
                    {
                        var imgPath = Path.Combine(webRootPath, img.ImgPath.Trim('\\'));
                        if (System.IO.File.Exists(imgPath))
                        {
                            System.IO.File.Delete(imgPath);
                        }
                    }
                }
                _db.BlogThread.Remove(thread);
            }
        }
    }
}
