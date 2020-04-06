using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Collections.Generic;
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
        public IEnumerable<SelectListItem> GetCommonAssetListForDropDown()
        {
            return _db.CommonAreaAssets.Select(i => new SelectListItem()
            {
                Text = i.AssetName,
                Value = i.Id.ToString()
            });
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