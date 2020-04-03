using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository
{
    public class BlogThreadRepository : Repository<BlogThread>, IBlogThreadRepository
    {
        private readonly ApplicationDbContext _db;
        public BlogThreadRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
