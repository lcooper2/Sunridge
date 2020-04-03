using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    internal class MaintenanceRepository : Repository<Maintenance>, IMaintenanceRepository
    {
        private ApplicationDbContext _db;

        public MaintenanceRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Maintenance maintenance)
        {
            var obJFromDb = _db.Maintenance.FirstOrDefault(s => s.Id == maintenance.Id);

            obJFromDb.CommonAreaAssetId = maintenance.CommonAreaAssetId;
            obJFromDb.Cost = maintenance.Cost;
            obJFromDb.DateCompleted = maintenance.DateCompleted;
            obJFromDb.CommonAreaAsset = maintenance.CommonAreaAsset;
            
            _db.SaveChanges();
        }
    }
}