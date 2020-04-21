using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IBlogImageRepository : IRepository<BlogImage>
    {
        public string GetWebRootPath();
    }
}
