using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository
{
    public class BannerRepository : Repository<Banner>, IBannerRepository
    {
        private readonly ApplicationDbContext _db;

        public BannerRepository (ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Banner banner)
        {
            var objFromDB = _db.Banner.FirstOrDefault(s => s.Id == banner.Id);

            objFromDB.Header = banner.Header;
            objFromDB.Body = banner.Body;
            objFromDB.Image = banner.Image;
            objFromDB.Status = banner.Status;
            _db.SaveChanges();
        }
    }
}
