using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository
{
    public class LikeRepository : Repository<Like>, ILikeRepository
    {
        private readonly ApplicationDbContext _db;
        public LikeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
