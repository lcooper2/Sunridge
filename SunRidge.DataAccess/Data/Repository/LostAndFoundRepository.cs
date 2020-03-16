using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository
{
   public class LostAndFoundRepository: Repository<LostAndFound>, ILostAndFoundRepository
    {
        private readonly ApplicationDbContext _db;

        public LostAndFoundRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(LostAndFound lostAndFound)
        {
            var objFromDb = _db.LostAndFound.FirstOrDefault(s => s.Id == lostAndFound.Id);

            objFromDb.ItemName = lostAndFound.ItemName;
            objFromDb.Discription = lostAndFound.Discription;
            objFromDb.ListedDate = lostAndFound.ListedDate;
            objFromDb.Claimed = lostAndFound.Claimed;
            objFromDb.ClaimedBy = lostAndFound.ClaimedBy;
            objFromDb.ClaimedDate = lostAndFound.ClaimedDate;

            _db.SaveChanges();
            

        }
    }
}
