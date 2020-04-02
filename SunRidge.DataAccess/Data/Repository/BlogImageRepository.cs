using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository
{
    public class BlogImageRepository : Repository<BlogImage>, IBlogImageRepository
    {
        private readonly ApplicationDbContext _db;
        public BlogImageRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
