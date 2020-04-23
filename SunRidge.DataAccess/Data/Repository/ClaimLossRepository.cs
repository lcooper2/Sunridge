using System;
using System.Collections.Generic;
using System.Text;
using Sunridge.Models;
using Sunridge.DataAccess.Data.Repository.IRepository;
using System.Linq;

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

            obJFromDb.isAttorney = claimLoss.isAttorney;
            obJFromDb.DateofIncident = claimLoss.DateofIncident;
            //obJFromDb.TimeofIncident = claimLoss.TimeofIncident;
            obJFromDb.Type = claimLoss.Type;
            obJFromDb.ApplicationUserId = claimLoss.ApplicationUserId;
            obJFromDb.ApplicationUser = claimLoss.ApplicationUser;
            obJFromDb.ClaimAddress = claimLoss.ClaimAddress;
            _db.SaveChanges();



        }
    }
}
