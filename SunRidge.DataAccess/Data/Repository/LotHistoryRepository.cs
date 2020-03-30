using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository
{
    public class LotHistoryRepository : Repository<LotHistory>, ILotHistoryRepository
    {
        private readonly ApplicationDbContext _db;

        public LotHistoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(LotHistory letHistory)
        {
            var obJFromDb = _db.LotHistory.FirstOrDefault(s => s.Id == letHistory.Id);

            obJFromDb.PrivacyLevel = letHistory.PrivacyLevel;
            obJFromDb.IsArchive = letHistory.IsArchive;
            obJFromDb.LastModifiedBy = letHistory.LastModifiedBy;
            obJFromDb.LastModifiedDate = letHistory.LastModifiedDate;
            obJFromDb.LotId = letHistory.LotId;
            obJFromDb.Lot = letHistory.Lot;

            _db.SaveChanges();

        }
    }
}
