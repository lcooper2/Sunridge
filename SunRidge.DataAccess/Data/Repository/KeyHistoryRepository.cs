using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository
{
    public class KeyHistoryRepository : Repository<KeyHistory>, IKeyHistoryRepository
    {
        private readonly ApplicationDbContext _db;

        public KeyHistoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(KeyHistory keyHistory)
        {
            var obJFromDb = _db.KeyHistory.FirstOrDefault(s => s.Id == keyHistory.Id);

            obJFromDb.Status = keyHistory.Status;
            obJFromDb.DateIssued = keyHistory.DateIssued;
            obJFromDb.DateReturned = keyHistory.DateReturned;
            obJFromDb.PaidAmount = keyHistory.PaidAmount;
            obJFromDb.KeyId = keyHistory.KeyId;
            obJFromDb.Key = keyHistory.Key;
            obJFromDb.LotId = keyHistory.LotId;
            obJFromDb.Lot = keyHistory.Lot;

            _db.SaveChanges();

        }
    }
}
