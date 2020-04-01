using System;
using System.Collections.Generic;
using System.Text;
using Sunridge.Models;
using Sunridge.DataAccess.Data.Repository.IRepository;

namespace Sunridge.DataAccess.Data.Repository
{
    public class ClaimLossRepository : Repository<ClaimLoss>, IClaimLossRepository
    {
        private readonly ApplicationDbContext _db;

        public ClaimLossRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ClaimLoss claimLoss)
        {
            var obJFromDb = _db.ClaimLoss.FirstOrDefault(s => s.Id == claimLoss.Id);

            obJFromDb.Activity = inKindWorkHours.Activity;
            obJFromDb.Equipment = inKindWorkHours.Equipment;
            obJFromDb.Hours = inKindWorkHours.Hours;
            obJFromDb.Type = inKindWorkHours.Type;
            obJFromDb.ApplicationUserId = inKindWorkHours.ApplicationUserId;
            obJFromDb.ApplicationUser = inKindWorkHours.ApplicationUser;
            _db.SaveChanges();



        }
    }
}
