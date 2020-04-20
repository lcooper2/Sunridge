using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository
{
    public class InventoryRepository : Repository<Inventory>, IInventoryRepository
    {
        private readonly ApplicationDbContext _db;

        public InventoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Inventory inventory)
        {
            var objFromDB = _db.Inventory.FirstOrDefault(s => s.Id == inventory.Id);

            objFromDB.Description = inventory.Description;
            objFromDB.IsArchive = inventory.IsArchive;
            objFromDB.LastModifiedBy = inventory.LastModifiedBy;
            objFromDB.LastModifiedDate = inventory.LastModifiedDate;
            //objFromDB.LotInventoryId = inventory.LotInventoryId;
            _db.SaveChanges();
        }
    }
}
