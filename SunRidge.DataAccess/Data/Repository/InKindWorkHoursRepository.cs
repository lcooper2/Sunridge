﻿using Sunridge.DataAccess.Data.Repository.IRepository;
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
        public IEnumerable<InKindWorkHours> GetWikListForDropDown()
        {
            return _db.InKindWorkHours.Select(i => new InKindWorkHours()
            {
                Activity = i.Activity,
                Id = i.Id,
                Equipment = i.Equipment,
                Hours = i.Hours,
                Type = i.Type,
                ApplicationUserId = i.ApplicationUserId,
                ApplicationUser = i.ApplicationUser,
            });
        }
        public void Update(InKindWorkHours inKindWorkHours)
        {
            var obJFromDb = _db.InKindWorkHours.FirstOrDefault(s => s.Id == inKindWorkHours.Id);

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
