using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    internal class CommonAreaAssetRepository : Repository<CommonAreaAsset>, ICommonAreaAssetRepository
    {
        private ApplicationDbContext _db;

        public CommonAreaAssetRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CommonAreaAsset commonAreaAsset)
        {
            var obJFromDb = _db.CommonAreaAssets.FirstOrDefault(s => s.Id == commonAreaAsset.Id);

            obJFromDb.AssetName = commonAreaAsset.AssetName;
            obJFromDb.PurchasePrice = commonAreaAsset.PurchasePrice;
            obJFromDb.Description = commonAreaAsset.Description;
            obJFromDb.Status = commonAreaAsset.Status;
            obJFromDb.Date = commonAreaAsset.Date;
            _db.SaveChanges();
        }
    }
}