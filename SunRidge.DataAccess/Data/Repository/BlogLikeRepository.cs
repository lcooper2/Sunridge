using Microsoft.EntityFrameworkCore;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Threading.Tasks;

namespace Sunridge.DataAccess.Data.Repository
{
    public class BlogLikeRepository : Repository<BlogLike>, IBlogLikeRepository
    {
        private readonly ApplicationDbContext _db;
        public BlogLikeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
