using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository
{
    public class ReplyRepository : Repository<Reply>, IReplyRepository
    {
        private readonly ApplicationDbContext _db;
        public ReplyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
