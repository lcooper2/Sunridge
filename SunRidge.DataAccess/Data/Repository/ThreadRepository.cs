using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository
{
    public class ThreadRepository : Repository<Thread>, IThreadRepository
    {
        private readonly ApplicationDbContext _db;
        public ThreadRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
