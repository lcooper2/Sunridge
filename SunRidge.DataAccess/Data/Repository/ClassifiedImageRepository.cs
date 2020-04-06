using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository
{
    public class ClassifiedImageRepository : Repository<ClassifiedImage>, IClassifiedImageRepository
    {
        private readonly ApplicationDbContext _db;

        public ClassifiedImageRepository (ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetImageListForDropDown(int? id)
        {
            return _db.ClassifiedImage.Where(c => c.ClassifiedListingId == id).Select(m => new SelectListItem()
            {
                Text = m.ImageURL,
                Value = m.Id.ToString()
            });
        }

        public void Update(ClassifiedImage classifiedImage)
        {
            var objFromDB = _db.ClassifiedImage.FirstOrDefault(s => s.Id == classifiedImage.Id);

            objFromDB.ClassifiedListingId = classifiedImage.ClassifiedListingId;
            objFromDB.ImageURL = classifiedImage.ImageURL;
            objFromDB.IsMainImage = classifiedImage.IsMainImage;
            _db.SaveChanges();
        }
    }
}
