using Sunridge.Models;

namespace Sunridge.DataAccess.Data.Repository.IRepository
{
    public interface ICommonAreaAssetRepository : IRepository<CommonAreaAsset>
    {
        public void Update(CommonAreaAsset commonAreaAsset);
    }
}