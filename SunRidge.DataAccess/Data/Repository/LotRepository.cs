using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository
{
    public class LotRepository : Repository<Lot>, ILotRepository
    {
        private readonly ApplicationDbContext _db;

        public LotRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Lot lot)
        {
            var obJFromDb = _db.Lot.FirstOrDefault(s => s.Id == lot.Id);

            obJFromDb.LotNumber = lot.LotNumber;
            obJFromDb.TaxId = lot.TaxId;
            obJFromDb.IsArchive = lot.IsArchive;
            obJFromDb.LastModifiedBy = lot.LastModifiedBy;
            obJFromDb.LastModifiedDate = lot.LastModifiedDate;
            //obJFromDb.AddressId = lot.AddressId;
           // obJFromDb.Address = lot.Address;
            

            _db.SaveChanges();
        }
    }
}
