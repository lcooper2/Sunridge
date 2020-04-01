using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sunridge.DataAccess.Data.Repository
{
    public class FormResponseRepository : Repository<FormResponse>, IFormResponseRepository
    {
        private readonly ApplicationDbContext _db;

        public FormResponseRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(FormResponse FormResponse)
        {
            var obJFromDb = _db.FormResponse.FirstOrDefault(s => s.Id == FormResponse.Id);

            _db.FormResponse.Update(obJFromDb);

            _db.SaveChanges();



        }
    }
}
