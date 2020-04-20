using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sunridge.DataAccess.Data.Repository
{
   public class UserPhotoCategoryRepository : Repository<UserPhotoCategory>, IUserPhotoCategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public UserPhotoCategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetCategoryListForDropDown()
        {
            return _db.UserPhotoCategory.Select(i => new SelectListItem()
            {
                Text = i.Title,
                Value = i.Id.ToString()
            });
        }

        public void Update(UserPhotoCategory userPhotoCategory)
        {
            var objFromDb = _db.UserPhotoCategory.FirstOrDefault(s => s.Id == userPhotoCategory.Id);

            objFromDb.Title = userPhotoCategory.Title;

            _db.SaveChanges();
        }
    }
}
