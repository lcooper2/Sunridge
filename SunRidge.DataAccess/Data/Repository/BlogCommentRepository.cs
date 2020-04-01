using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository
{
    public class BlogCommentRepository : Repository<BlogComment>, IBlogCommentRepository
    {
        private readonly ApplicationDbContext _db;
        public BlogCommentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
