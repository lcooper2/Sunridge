using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository
{
    public class BlogReplyRepository : Repository<BlogReply>, IBlogReplyRepository
    {
        private readonly ApplicationDbContext _db;
        public BlogReplyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
