using Sunridge.Models;
using System.Collections.Generic;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IBlogThreadRepository : IRepository<BlogThread>
    {
        public List<BlogThread> LoadAll();
        public void DeleteThread(int id);
    }
}
