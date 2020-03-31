using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository
{
    public class InKindWorkHoursRepository : Repository<InKindWorkHours>, IInKindWorkHoursRepository
    {
        private readonly ApplicationDbContext _db;

        public InKindWorkHoursRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(InKindWorkHours inKindWorkHours)
        {
            var obJFromDb = _db.InKindWorkHours.FirstOrDefault(s => s.Id == inKindWorkHours.Id);

            obJFromDb.Description = inKindWorkHours.Description;
            obJFromDb.Hours = inKindWorkHours.Hours;
            obJFromDb.Type = inKindWorkHours.Type;
            obJFromDb.FormResponseId = inKindWorkHours.FormResponseId;

            _db.SaveChanges();



        }
    }
}
