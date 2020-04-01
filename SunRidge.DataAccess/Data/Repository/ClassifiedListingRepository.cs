using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository
{
    public class ClassifiedListingRepository : Repository<ClassifiedListing>, IClassifiedListingRepository
    {
        private readonly ApplicationDbContext _db;

        public ClassifiedListingRepository (ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ClassifiedListing classifiedListing)
        {
            var objFromDB = _db.ClassifiedListing.FirstOrDefault(s => s.Id == classifiedListing.Id);

            objFromDB.UserId = classifiedListing.UserId;
            objFromDB.Description = classifiedListing.Description;
            objFromDB.ItemName = classifiedListing.ItemName;
            objFromDB.ListingDate = classifiedListing.ListingDate;
            objFromDB.Price = classifiedListing.Price;
            objFromDB.ClassifiedCategoryId = classifiedListing.ClassifiedCategoryId;
            _db.SaveChanges();
        }
    }
}
