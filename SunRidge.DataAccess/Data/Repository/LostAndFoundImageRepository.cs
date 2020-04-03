using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository
{
    public class LostAndFoundImageRepository : Repository<LostAndFoundImage>, ILostAndFoundImageRepository
    {
        private readonly ApplicationDbContext _db;

        public LostAndFoundImageRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(LostAndFoundImage lostAndFoundImage)
        {
            var objFromDB = _db.LostAndFoundImage.FirstOrDefault(s => s.Id == lostAndFoundImage.Id);

            objFromDB.LostAndFoundId = lostAndFoundImage.LostAndFoundId;
            objFromDB.ImageURL = lostAndFoundImage.ImageURL;
            objFromDB.IsMainImage = lostAndFoundImage.IsMainImage;
            _db.SaveChanges();
        }
    }
}
