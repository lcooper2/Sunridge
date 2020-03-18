using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db;

        public ApplicationUserRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(ApplicationUser applicationUser)
        {
            var personFromDB = _db.ApplicationUser.FirstOrDefault(s => s.Id == applicationUser.Id);


            personFromDB.FirstName = applicationUser.FirstName;
            personFromDB.LastName = applicationUser.LastName;
            personFromDB.Occupation = applicationUser.Occupation;
            personFromDB.PhoneNumber = applicationUser.PhoneNumber;
            personFromDB.Email = applicationUser.Email;
            personFromDB.EmergencyContactName = applicationUser.EmergencyContactName;
            personFromDB.EmergencyContactPhone = applicationUser.EmergencyContactPhone;
            personFromDB.ReceiveEmails = applicationUser.ReceiveEmails;
            personFromDB.IsArchive = applicationUser.IsArchive;

            _db.SaveChanges();

        }


    }
}
