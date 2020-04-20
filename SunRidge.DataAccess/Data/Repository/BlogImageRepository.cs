using Microsoft.AspNetCore.Hosting;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository
{
    public class BlogImageRepository : Repository<BlogImage>, IBlogImageRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BlogImageRepository(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment) : base(db)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public string GetWebRootPath()
        {
            return _webHostEnvironment.WebRootPath;
        }
    }
}
