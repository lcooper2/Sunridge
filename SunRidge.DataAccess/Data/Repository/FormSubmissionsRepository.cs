using Microsoft.AspNetCore.Mvc.Rendering;
using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    internal class FormSubmissionsRepository : Repository<FormSubmissions>, IFormSubmissionsRepository
    {
        private ApplicationDbContext _db;

        public FormSubmissionsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetFormSubmissionsListForDropDown()
        {
            return _db.FormSubmissions.Select(i => new SelectListItem()
            {
                Text = i.FormId,
                Value = i.Id.ToString()
            });
        }

        public void Update(FormSubmissions FormSubmissions)
        {
            var obJFromDb = _db.FormSubmissions.FirstOrDefault(s => s.Id == FormSubmissions.Id);
            obJFromDb.SubmitDate = FormSubmissions.SubmitDate;
           // obJFromDb.FormType = FormSubmissions.FormType;
            obJFromDb.FormId = FormSubmissions.FormId;
            obJFromDb.IsCl = FormSubmissions.IsCl;
            obJFromDb.IsWik = FormSubmissions.IsWik;
            obJFromDb.IsSC = FormSubmissions.IsSC;
            // obJFromDb.ClaimLoss = FormSubmissions.ClaimLoss;

            _db.SaveChanges();
        }
    }
}