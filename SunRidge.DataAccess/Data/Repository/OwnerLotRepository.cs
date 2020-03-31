using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    internal class OwnerLotRepository : Repository<OwnerLot>, IOwnerLotRepository
    {
        private ApplicationDbContext _db;

        public OwnerLotRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OwnerLot ownerLot)
        {
            var obJFromDb = _db.OwnerLot.FirstOrDefault(s => s.Id == ownerLot.Id);

            obJFromDb.IsPrimary = ownerLot.IsPrimary;
            obJFromDb.StartDate = ownerLot.StartDate;
            obJFromDb.EndDate = ownerLot.EndDate;
            obJFromDb.ApplicationUser = ownerLot.ApplicationUser;
            obJFromDb.ApplicationUserId = ownerLot.ApplicationUserId;
            obJFromDb.Lot = ownerLot.Lot;
            obJFromDb.LotId = ownerLot.LotId;

            _db.SaveChanges();

        }
    }
}