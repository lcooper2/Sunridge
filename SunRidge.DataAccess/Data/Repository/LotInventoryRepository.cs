using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository
{
    public class LotInventoryRepository : Repository<LotInventory>, ILotInventoryRepository
    {
        private readonly ApplicationDbContext _db;

        public LotInventoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(LotInventory lotInventory)
        {
            var obJFromDb = _db.LotInventory.FirstOrDefault(s => s.Id == lotInventory.Id);

            obJFromDb.Description = lotInventory.Description;
            obJFromDb.IsArchive = lotInventory.IsArchive;
            obJFromDb.LastModifiedBy = lotInventory.LastModifiedBy;
            obJFromDb.LastModifiedDate = lotInventory.LastModifiedDate;
            obJFromDb.LotId = lotInventory.LotId;
            obJFromDb.InventoryId = lotInventory.InventoryId;

            _db.SaveChanges();

        }
    }
}
