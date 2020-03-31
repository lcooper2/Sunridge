using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface IFileRepository: IRepository<File>
    {
        public void Update(File file);

    }
}