using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface ICommentRepository : IRepository<Comment>
    {
        public void Update(Comment comment);

    }
}