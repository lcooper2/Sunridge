using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository
{
    public class UserPhotosRepository : Repository<UserPhotos>, IUserPhotosRepository
    {
        private readonly ApplicationDbContext _db;

        public UserPhotosRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(UserPhotos userPhotos)
        {
            var objFromDb = _db.UserPhotos.FirstOrDefault(s => s.Id == userPhotos.Id);

            objFromDb.UserId = userPhotos.UserId;
            objFromDb.CategoryId = userPhotos.CategoryId;
            objFromDb.Title = userPhotos.Title;
            objFromDb.Year = userPhotos.Year;
            if (userPhotos.Image != null) { 
            objFromDb.Image = userPhotos.Image;
            }


        }
    }
}
