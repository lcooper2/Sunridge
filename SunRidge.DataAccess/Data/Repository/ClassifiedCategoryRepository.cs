using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository
{
    public class ClassifiedCategoryRepository : Repository<ClassifiedCategory>, IClassifiedCategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public ClassifiedCategoryRepository (ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetCategoryListForDropDown()
        {
            return _db.ClassifiedCategory.Select(m => new SelectListItem()
            {
                Text = m.Description,
                Value = m.Id.ToString()
            });
        }

        public void Update(ClassifiedCategory classifiedCategory)
        {
            var objFromDB = _db.ClassifiedCategory.FirstOrDefault(s => s.Id == classifiedCategory.Id);

            objFromDB.Description = classifiedCategory.Description;
            _db.SaveChanges();
        }
    }
}
